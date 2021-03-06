﻿using System.Collections.Generic;
using System.Linq;
using TShockAPI;

namespace Statistics
{
    public class HighScores
    {
        public readonly List<HighScore> highScores = new List<HighScore>();

        public void DisplayHighScores(TSPlayer player, int page = 1)
        {
            var ordered = (from x in highScores orderby -x.score select x).ToList();

            var hs = ordered.ToDictionary(obj => obj.name, obj => obj.score);

            HsPagination.SendPage(player, page, hs, new HsPagination.FormatSettings
            {
                FooterFormat = "use /hs {0} for more high scores", FooterTextColor = Color.Lime,
                HeaderFormat = "High Scores- Page {0} of {1}", HeaderTextColor = Color.Lime,
                IncludeFooter = true, IncludeHeader = true, MaxLinesPerPage = 5,
                NothingToDisplayString = "No highscores available"
            });
        }
    }

    public class HighScore
    {
        public readonly string name;
        public int score;

        public HighScore(string name, int kills, int mobKills, int deaths, int bossKills, int time)
        {
            this.name = name;
            score = ((2*kills) + mobKills + (3*bossKills))/(deaths == 0 ? 1 : deaths);
            score += (time/60);
        }

        public void UpdateHighScore(int kills, int mobKills, int deaths, int bossKills, int time)
        {
            score = ((2*kills) + mobKills + (3*bossKills))/(deaths == 0 ? 1 : deaths);
            score += (time/60);
        }
    }
}
