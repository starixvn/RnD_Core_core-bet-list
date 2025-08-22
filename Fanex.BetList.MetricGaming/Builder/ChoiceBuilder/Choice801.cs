namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Goal Within 60 Seconds
    /// </summary>
    public class Choice801 : Choice1
    {
        protected const string MetricGamingResourceId = "SuperLive";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticketHelper.GetResourceData(MetricGamingResourceId, GetBetChoice(ticket));
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = GetBetTypeTime(ticket);
        }

        protected virtual string GetBetChoice(ITicket ticket)
        {
            return GetTransDescElementByName(ticket, "betchoice");
        }

        protected virtual string GetBetTypeTime(ITicket ticket)
        {
            switch (ticket.Status.ToLowerInvariant())
            {
                case BetStatus.Won:
                case BetStatus.Lose:
                case BetStatus.Draw:
                    return GetTransDescElementByName(ticket, "bettypetime");

                default:
                    return string.Empty;
            }
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected string GetTransDescElementByName(ITicket ticket, string name)
        {
            if (!string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                var descriptions = ticket.TransDesc.Split(';');
                var matchingDescription = Array.Find(descriptions, description => description.StartsWith(name, StringComparison.InvariantCultureIgnoreCase));

                if (matchingDescription != null)
                {
                    var indexOfEqual = matchingDescription.IndexOf('=') + 1;
                    return matchingDescription.Substring(indexOfEqual, matchingDescription.Length - indexOfEqual);
                }
            }

            return string.Empty;
        }
    }
}