using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Cs6065_Homework2.Models;
using Cs6065_Homework2.Services;

namespace Cs6065_Homework2.Controllers
{
    [Route("Player")]
    public class NflPlayerController : Controller
    {
        private readonly NflPlayerService _nflPlayerService;

        public NflPlayerController(NflPlayerService nflPlayerService)
        {
            _nflPlayerService = nflPlayerService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("Quarterback/Json")]
        public async Task<JsonResult> QuarterbackJson()
        {
            var players = await _nflPlayerService.GetQuarterbacksAsync();
            return Json(players, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                });
        }
        [Route("RunningBack/Json")]
        public async Task<JsonResult> RunningBackJson()
        {
            var players = await _nflPlayerService.GetRunningBacksAsync();
            return Json(players, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                });
        }
        [Route("TightEnd/Json")]
        public async Task<JsonResult> TightEndJson()
        {
            var players = await _nflPlayerService.GetTightEndsAsync();
            return Json(players, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                });
        }
        [Route("WideReceiver/Json")]
        public async Task<JsonResult> WideReceiverJson()
        {
            var players = await _nflPlayerService.GetWideReceiversAsync();
            return Json(players, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                });
        }
    }
}
