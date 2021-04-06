using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            context = con;
            
        }

        public IActionResult Index(long? teamid, int pageNum = 0)
        {
            //Setting the number of bowlers to appear on the page to 5
            int pageSize = 5;
            return View(new IndexViewModel
            {
                //Creating a new Viewmodel and populating it from the tables so that the data can be passed to the index.cshtml
                Bowlers = (context.Bowlers
                .Where(b => b.TeamId == teamid || teamid == null)
                .OrderBy(b => b.BowlerLastName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                //Creating new Pagination information, because we want to it to change with the bowler count
                //especially when team is selected
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                    context.Bowlers.Where(t => t.TeamId == teamid).Count())
                },
                //Setting the teams here, and selecting the infomration so we can use it to show the team at the top of the list
                //when a team is selected
                Teams = (context.Teams
                .FromSqlInterpolated($"SELECT * FROM Teams WHERE TeamID = {teamid}")),
                CurrentTeam = teamid
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
