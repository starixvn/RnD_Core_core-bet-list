namespace Fanex.BetList.Core.Utils.TicketHelperExt
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public static class ScoreBoxExtensions
    {
        internal class ScoreOdds
        {
            public int Score { get; set; }

            public double Odds { get; set; }
        }

        public const string ScoreBoxRefId = "ScoreBox";

        private const string OverBetOption = "O";
        private const string UnderBetOption = "U";

        private const string OddsFormat = "0.00";
        private const string HavingMorePattern = "+";
        private const string HavingLessPattern = "-";
        private const string PluralFormPattern = "S";

        public static string[] GetScoreBoxOdds(this ITicketHelper ticketHelper, string betTeam, string betCheck)
        {
            var standardBetTeam = betTeam.Trim().ToUpperInvariant();
            var scoreBoxOddsMappings = ParseScoreOddsMappings(betCheck);
            var scoreBoxOddsDescriptions = IsOverUnder(standardBetTeam)
                ? GetTotalGoalsOddsDescriptions(ticketHelper, standardBetTeam, scoreBoxOddsMappings)
                : GetWinScoreOddsDescriptions(ticketHelper, standardBetTeam, scoreBoxOddsMappings);

            return scoreBoxOddsDescriptions.ToArray();
        }

        private static bool IsOverUnder(string betTeam)
            => betTeam == OverBetOption || betTeam == UnderBetOption;

        internal static List<ScoreOdds> ParseScoreOddsMappings(string betCheck)
        {
            var scoreOddsMappings = new List<ScoreOdds>();
            var scoreOddsPairs = betCheck.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var scoreOddsPair in scoreOddsPairs)
            {
                const int expectedNumberOfSplittedFields = 2;

                var splittedFields = scoreOddsPair.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (splittedFields.Length == expectedNumberOfSplittedFields)
                {
                    int.TryParse(splittedFields[0].Trim(), out int score);
                    double.TryParse(splittedFields[1].Trim(), out double odds);

                    scoreOddsMappings.Add(new ScoreOdds
                    {
                        Score = score,
                        Odds = odds
                    });
                }
            }

            return scoreOddsMappings;
        }

        internal static List<string> GetTotalGoalsOddsDescriptions(ITicketHelper ticketHelper, string betTeam, List<ScoreOdds> totalGoalsOddsMappings)
        {
            var totalGoalsOddsDescriptions = new List<string>();
            var lastIndex = totalGoalsOddsMappings.Count - 1;

            for (var index = 0; index <= lastIndex; index++)
            {
                var isFirst = index == 0;
                var isLast = (index > 0) && (index == lastIndex);
                var totalGoals = totalGoalsOddsMappings[index].Score;
                var builtResourceId = BuildTotalGoalsOddsResourceId(totalGoals, isFirst, isLast);
                var descriptionPattern = ticketHelper.GetResourceData(ScoreBoxRefId, builtResourceId);

                if (!string.IsNullOrEmpty(descriptionPattern))
                {
                    var formattedOdds = totalGoalsOddsMappings[index].Odds.ToString(OddsFormat);
                    var description = string.Format(descriptionPattern, totalGoals, formattedOdds);

                    totalGoalsOddsDescriptions.Add(description);
                }
            }

            if (betTeam == UnderBetOption)
            {
                totalGoalsOddsDescriptions.Reverse();
            }

            return totalGoalsOddsDescriptions;
        }

        private static string BuildTotalGoalsOddsResourceId(int totalScores, bool isFirst, bool isLast)
        {
            const string intialPattern = "O";

            var builtResourceId = intialPattern;

            if (IsPlural(totalScores))
            {
                builtResourceId += PluralFormPattern;
            }

            if (isFirst)
            {
                builtResourceId += HavingMorePattern;
            }

            if (isLast && totalScores > 0)
            {
                builtResourceId += HavingLessPattern;
            }

            return builtResourceId;
        }

        internal static List<string> GetWinScoreOddsDescriptions(ITicketHelper ticketHelper, string betTeam, List<ScoreOdds> winScoreOddsMappings)
        {
            const string awayBetOption = "A";

            var winScoreOddsDescriptions = new List<string>();
            var lastIndex = winScoreOddsMappings.Count - 1;

            if (betTeam == awayBetOption)
            {
                winScoreOddsMappings.Reverse();
                winScoreOddsMappings.ForEach(item => item.Score = item.Score * -1);
            }

            for (var index = 0; index <= lastIndex; index++)
            {
                var isFirst = index == 0;
                var isLast = (index > 0) && (index == lastIndex);
                var score = winScoreOddsMappings[index].Score;
                var builtResourceId = BuildWinScoreOddsResourceId(score, isFirst, isLast);
                var descriptionPattern = ticketHelper.GetResourceData(ScoreBoxRefId, builtResourceId);

                if (!string.IsNullOrEmpty(descriptionPattern))
                {
                    var absoluteScore = Math.Abs(score);
                    var formattedOdds = winScoreOddsMappings[index].Odds.ToString(OddsFormat);
                    var description = string.Format(descriptionPattern, absoluteScore, formattedOdds);

                    winScoreOddsDescriptions.Add(description);
                }
            }

            return winScoreOddsDescriptions;
        }

        private static string BuildWinScoreOddsResourceId(int score, bool isFirst, bool isLast)
        {
            const string drawResourceId = "D";
            const string intialPattern = "H";
            const string winPattern = "W";
            const string losePattern = "L";

            if (score == 0)
            {
                return drawResourceId;
            }

            var builtResourceId = intialPattern;

            builtResourceId += score > 0 ? winPattern : losePattern;

            if (IsPlural(score))
            {
                builtResourceId += PluralFormPattern;
            }

            if ((isFirst && score > 0) || (isLast && score < 0))
            {
                builtResourceId += HavingMorePattern;
            }

            return builtResourceId;
        }

        private static bool IsPlural(int score)
        {
            const int negativePluralComparer = -2;
            const int positivePluralComparer = 2;

            return score >= positivePluralComparer || score <= negativePluralComparer;
        }
    }
}