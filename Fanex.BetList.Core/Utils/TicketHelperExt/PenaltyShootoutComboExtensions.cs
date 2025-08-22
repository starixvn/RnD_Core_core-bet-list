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
            if (string.IsNullOrEmpty(ticket.BetTeam) || string.IsNullOrEmpty(ticket.BetCheck))
            {
                yield break;
            }

            var options = ticket.BetTeam.ToCharArray();
            var odds = ticket.BetCheck.Split(',');
            var numberOfRound = Math.Max(options.Length, odds.Length) / 2;
            var awayIndex = numberOfRound;
            
            for (var i = 0; i < numberOfRound; i++, awayIndex++)
            {
                var roundOptions = CreateRoundOptions(i, awayIndex, options, odds, ticket, ticketHelper);
                foreach (var option in roundOptions)
                {
                    yield return option;
                }
            }
        }

        private static IEnumerable<RoundOption> CreateRoundOptions(int roundIndex, int awayIndex, char[] options, string[] odds, ITicket ticket, ITicketHelper ticketHelper)
        {
            var oddsHome = GetOddsAtIndex(odds, roundIndex);
            var optionHome = GetOptionAtIndex(options, roundIndex);
            var oddsAway = GetOddsAtIndex(odds, awayIndex);
            var optionAway = GetOptionAtIndex(options, awayIndex);
            
            var homeName = ticketHelper.GetTeamNameById(ticket.HomeId);
            var awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            yield return new RoundOption(roundIndex + 1, optionHome, oddsHome, homeName);
            yield return new RoundOption(roundIndex + 1, optionAway, oddsAway, awayName);
        }

        private static string GetOddsAtIndex(string[] odds, int index)
        {
            return index < odds.Length ? odds[index].Trim() : string.Empty;
        }

        private static string GetOptionAtIndex(char[] options, int index)
        {
            return index < options.Length ? options[index].ToString().ToLowerInvariant() : string.Empty;
        }

        public class RoundOption
        {
            public RoundOption(int round, string option, string odds, string teamName)
            {
                Round = round;
                Option = option;
                OddsString = odds;
                TeamName = teamName;
            }

            public int Round { get; }

            public string TeamName { get; }

            public string Option { get; }

            public string OddsString { get; }

            public string Text => GetDisplayText();

            private string GetDisplayText()
            {
                if (string.IsNullOrEmpty(Option) || Option == "-" || string.IsNullOrEmpty(OddsString))
                {
                    return string.Empty;
                }

                var text = GetOptionText(Option);
                if (string.IsNullOrEmpty(text))
                {
                    return string.Empty;
                }

                double.TryParse(OddsString, out double odds);
                return string.Format(text, TeamName, Round, string.Format(Formatter.DECIMALFORMAT2C, odds));
            }

            private static string GetOptionText(string option)
            {
                switch (option)
                {
                    case BetTeamValue.S:
                        return CoreBetList.TeamNameRoundGoal;
                    case BetTeamValue.M:
                        return CoreBetList.TeamNameRoundMiss;
                    default:
                        return string.Empty;
                }
            }
        }
    }
}