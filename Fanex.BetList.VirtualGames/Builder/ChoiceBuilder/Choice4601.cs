namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice4601 : Choice1
    {
        public const string ChoiceRefId = "VirtualGamesChoice";
        public const string TableRefId = "VirtualGamesTable";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betChoiceId = GetBetChoiceId(ticket.TransDesc);

            Template.betTeam = string.IsNullOrEmpty(betChoiceId) ? string.Empty : ticketHelper.GetResourceData(ChoiceRefId, betChoiceId);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var tableCode = GetTableCode(ticket.BetCheck);

            if (string.IsNullOrEmpty(tableCode))
            {
                Template.League.LeagueName.leagueName = string.Empty;
            }
            else
            {
                var tableName = ticketHelper.GetResourceData(TableRefId, tableCode);

                Template.League.LeagueName.leagueName = $"{tableName} [{ticket.BetCheck}]";
            }
        }

        private static string GetBetChoiceId(string transDesc)
        {
            if (!string.IsNullOrEmpty(transDesc))
            {
                const string betChoiceIndicator = "betChoice=";

                var indexOfBetChoiceIndicator = transDesc.IndexOf(betChoiceIndicator, StringComparison.InvariantCultureIgnoreCase);

                if (indexOfBetChoiceIndicator >= 0)
                {
                    var separators = new[] { ';', ',' };
                    var indexOfChoiceId = indexOfBetChoiceIndicator + betChoiceIndicator.Length;
                    var separatorIndex = transDesc.IndexOfAny(separators, indexOfChoiceId);
                    var betChoiceIdLength = (separatorIndex > 0 ? separatorIndex : transDesc.Length) - indexOfChoiceId;

                    return transDesc.Substring(indexOfChoiceId, betChoiceIdLength);
                }
            }

            return string.Empty;
        }

        protected static string GetTableCode(string betCheck)
        {
            const int tableCodeIndex = 1;
            const int tableCodeLength = 3;

            return string.IsNullOrEmpty(betCheck) || betCheck.Length < tableCodeIndex + tableCodeLength
                ? string.Empty
                : betCheck.Substring(tableCodeIndex, tableCodeLength);
        }
    }
}