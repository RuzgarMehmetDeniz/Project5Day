using System;
using System.Collections.Generic;

namespace Project5Day.WebUI.Dtos.MatchDetailDto
{
    public class ResultMatchMegaDetailDto
    {
        public int MatchId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
        public string Attendance { get; set; }
        public string Referee { get; set; }
        public string League { get; set; }
        public string Sezon { get; set; }
        public int Week { get; set; }
        public string MatchTime { get; set; }
        public DateTime MatchDate { get; set; }

        // İstatistikler
        public int HomePossession { get; set; }
        public int AwayPossession { get; set; }
        public int HomeShots { get; set; }
        public int AwayShots { get; set; }
        public int HomeShotsOnTarget { get; set; }
        public int AwayShotsOnTarget { get; set; }
        public int HomePasses { get; set; }
        public int AwayPasses { get; set; }
        public int HomePassAccuracy { get; set; }
        public int AwayPassAccuracy { get; set; }
        public int HomeCorners { get; set; }
        public int AwayCorners { get; set; }
        public int HomeFouls { get; set; }
        public int AwayFouls { get; set; }
        public int HomeOffsides { get; set; }
        public int AwayOffsides { get; set; }

        public List<MegaEventDto> Events { get; set; }

        public class MegaEventDto
        {
            public int Minute { get; set; }
            public int Type { get; set; }
            public bool IsHomeTeam { get; set; }
            public string PlayerName { get; set; }
            public string EventDetail { get; set; }
            public string PlayerIn { get; set; }
            public string PlayerOut { get; set; }
        }
    }
}