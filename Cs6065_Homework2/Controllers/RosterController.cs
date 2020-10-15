#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Cs6065_Homework2.Models;
using Cs6065_Homework2.Models.ViewModels;
using Cs6065_Homework2.Services;

namespace Cs6065_Homework2.Controllers
{
    public class RosterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RosterService _rosterService;
        private readonly NflPlayerService _nflPlayerService;

        public RosterController(UserManager<ApplicationUser> userManager,
            RosterService rosterService,
            NflPlayerService nflPlayerService)
        {
            _userManager = userManager;
            _rosterService = rosterService;
            _nflPlayerService = nflPlayerService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            FantasyRoster? roster = await _rosterService.GetRosterForUserIdAsync(currentUser.Id);
            if (roster == null)
            {
                return RedirectToAction("Build");
            }
            return View($"~/Views/{RouteData.Values["controller"]}/ViewRoster.cshtml", roster);
        }

        [Route("Roster/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            ApplicationUser? queriedUser = null;
            try
            {
                queriedUser = await _userManager.Users
                    .Where(user => user.UserName == username)
                    .SingleAsync();
            }
            catch { }
            // queried user not found; return error
            if (queriedUser == null)
            {
                return View("~/Views/Shared/UserNotFound.cshtml", new UserNotFoundViewModel
                    {
                        ViewDataTitle = "Fantasy Roster",
                        Username = username
                    });
            }
            // queried user found: get roster & display
            FantasyRoster? roster = await _rosterService.GetRosterForUserIdAsync(queriedUser.Id);
            return View("~/Views/Roster/ViewRoster.cshtml", roster);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Build()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            
            var qbs = await _nflPlayerService.GetQuarterbacks().ToListAsync();
            var rbs = await _nflPlayerService.GetRunningBacks().ToListAsync();
            var tes = await _nflPlayerService.GetTightEnds().ToListAsync();
            var wrs = await _nflPlayerService.GetWideReceivers().ToListAsync();

            var viewModel = new BuildFantasyRosterViewModel
                {
                    Quarterbacks = qbs,
                    RunningBacks = rbs,
                    TightEnds = tes,
                    WideReceivers = wrs
                };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Build(BuildFantasyRosterViewModel model)
        {
            // TODO: this method has some awful repeated code, rebuilding the
            // viewmodel in the error condition which hits the database a "lot".
            // i don't yet know if it can be improved.

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            if (model.RunningBack1Id == model.RunningBack2Id)
            {
                ModelState.AddModelError(String.Empty, "You can't have two of the same running backs.");
            }
            if (model.WideReceiver1Id == model.WideReceiver2Id)
            {
                ModelState.AddModelError(String.Empty, "You can't have two of the same wide receivers.");
            }
            if (ModelState.ErrorCount != 0)
            {
                model.Quarterbacks = await _nflPlayerService.GetQuarterbacks().ToListAsync();
                model.RunningBacks = await _nflPlayerService.GetRunningBacks().ToListAsync();
                model.TightEnds = await _nflPlayerService.GetTightEnds().ToListAsync();
                model.WideReceivers = await _nflPlayerService.GetWideReceivers().ToListAsync();
                return View(model);
            }

            var qb = await _nflPlayerService.GetQuarterbackByIdAsync(model.QuarterbackId);
            var rb1 = await _nflPlayerService.GetRunningBackByIdAsync(model.RunningBack1Id);
            var rb2 = await _nflPlayerService.GetRunningBackByIdAsync(model.RunningBack2Id);
            var te = await _nflPlayerService.GetTightEndByIdAsync(model.TightEndId);
            var wr1 = await _nflPlayerService.GetWideReceiverByIdAsync(model.WideReceiver1Id);
            var wr2 = await _nflPlayerService.GetWideReceiverByIdAsync(model.WideReceiver2Id);

            if (qb == null || rb1 == null || rb2 == null || te == null || wr1 == null || wr2 == null)
            {
                ModelState.AddModelError(String.Empty, "there was an error with one or more submitted player ids.");
                model.Quarterbacks = await _nflPlayerService.GetQuarterbacks().ToListAsync();
                model.RunningBacks = await _nflPlayerService.GetRunningBacks().ToListAsync();
                model.TightEnds = await _nflPlayerService.GetTightEnds().ToListAsync();
                model.WideReceivers = await _nflPlayerService.GetWideReceivers().ToListAsync();
                return View(model);
            }

            FantasyRoster roster = new FantasyRoster
                {
                    Owner = currentUser,
                    OwnerId = currentUser.Id,
                    QuarterbackId = model.QuarterbackId,
                    Quarterback = qb,
                    RunningBack1Id = model.RunningBack1Id,
                    RunningBack1 = rb1,
                    RunningBack2Id = model.RunningBack2Id,
                    RunningBack2 = rb2,
                    TightEndId = model.TightEndId,
                    TightEnd = te,
                    WideReceiver1Id = model.WideReceiver1Id,
                    WideReceiver1 = wr1,
                    WideReceiver2Id = model.WideReceiver2Id,
                    WideReceiver2 = wr2
                };
            
            bool success = await _rosterService.AddOrUpdateRosterAsync(roster);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            var result = await _rosterService.DeleteRosterForUserIdAsync(currentUser.Id);
            if (result == true)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); // either way we'll go to index v0v
        }
    }
}
