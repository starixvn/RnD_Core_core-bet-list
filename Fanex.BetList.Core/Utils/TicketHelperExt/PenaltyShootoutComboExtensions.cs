namespace Fanex.BetList.Core.Utils.TicketHelperExt
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    public static class PenaltyShootoutComboExtensions
    {
        public static IEnumerable<RoundOption> GetRoundInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (!string.IsNullOrEmpty(ticket.BetTeam) && !string.IsNullOrEmpty(ticket.BetCheck))
            {
                var options = ticket.BetTeam.ToCharArray();
                var odds = ticket.BetCheck.Split(',');
                var numberOfRound = Math.Max(options.Length, odds.Length) / 2;
                var awayIndex = numberOfRound;
                for (var i = 0; i < numberOfRound; i++, awayIndex++)
                {
                    var oddsHome = i < odds.Length ? odds[i].Trim() : string.Empty;
                    var optionHome = i < options.Length ? options[i].ToString().ToLowerInvariant() : string.Empty;
                    var oddsAway = awayIndex < odds.Length ? odds[awayIndex].Trim() : string.Empty;
                    var optionAway = awayIndex < options.Length ? options[awayIndex].ToString().ToLowerInvariant() : string.Empty;
                    var homeName = ticketHelper.GetTeamNameById(ticket.HomeId);
                    var awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

                    yield return new RoundOption(i + 1, optionHome, oddsHome, homeName);
                    yield return new RoundOption(i + 1, optionAway, oddsAway, awayName);
                }
            }
        }

        public class RoundOption
        {
            public RoundOption(int round, string option, string odds, string teamName)
            {
                Round = round;
                Option = option;
                Odds = odds;
                TeamName = teamName;
            }

            public int Round { get; }

            public string TeamName { get; }

            public string Option { get; }

            public string Odds { get; }

            public string Text => GetDisplayText();

            private string GetDisplayText()
            {
                var text = string.Empty;

                if (string.IsNullOrEmpty(Option) || Option == "-" || string.IsNullOrEmpty(Odds))
                    return text;

                if (Option == BetTeamValue.S)
                {
                    text = CoreBetList.TeamNameRoundGoal;
                }

                if (Option == BetTeamValue.M)
                {
                    text = CoreBetList.TeamNameRoundMiss;
                }

                return string.Format(text, TeamName, Round, Odds);
            }
        }
    }
}