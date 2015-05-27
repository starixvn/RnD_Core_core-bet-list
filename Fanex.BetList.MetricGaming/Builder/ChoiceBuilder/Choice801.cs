namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice801 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticketHelper.GetResourceData(GetResourceId(), GetBetChoice(ticket));
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
            return GetTransDescElementByName(ticket, "bettypetime");
        }

        protected virtual string GetResourceId()
        {
            return "SuperLive";
        }

        private string GetTransDescElementByName(ITicket ticket, string name)
        {
            if (!string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                var descriptions = ticket.TransDesc.Split(';');
                foreach (var description in descriptions)
                {
                    if (description.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var indexOfEqual = description.IndexOf('=') + 1;

                        return description.Substring(indexOfEqual, description.Length - indexOfEqual);
                    }
                }
            }

            return string.Empty;
        }
    }
}
