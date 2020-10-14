#nullable enable
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public RosterController(UserManager<ApplicationUser> userManager,
            RosterService rosterService)
        {
            _userManager = userManager;
            _rosterService = rosterService;
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
                return View($"~/Views/{RouteData.Values["controller"]}/BuildRoster.cshtml", null);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoster()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            var result = await _rosterService.DeleteRosterForUserIdAsync(currentUser.Id);
            if (result == true)
            {
                return View(null);
            }
            return View(_rosterService.GetRosterForUserIdAsync(currentUser.Id));
        }
    }
}
