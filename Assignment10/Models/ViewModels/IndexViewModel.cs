using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    public class IndexViewModel
    {

        //Allowing for information to be passed to the home controller from both the Bowlers table and the teams table
        public IEnumerable<Bowler> Bowlers { get; set; }

        public IEnumerable<Team> Teams { get; set; }
        public PageNumberingInfo PageNumberingInfo { get; set; }
        public long? CurrentTeam { get; set; }
    }
}
