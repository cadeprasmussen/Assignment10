using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class TeamNameViewComponent : ViewComponent
    {
        public TeamNameViewComponent(BowlingLeagueContext con)
        {
            context = con;
        }

        private BowlingLeagueContext context;

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["teamname"];
            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
