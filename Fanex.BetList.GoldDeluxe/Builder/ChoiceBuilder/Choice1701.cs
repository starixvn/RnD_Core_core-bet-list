namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    public class Choice1701 : Choice1101
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match.Hide();
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.Hide();
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = GetBetTypeName(ticket.BetTypeId, ticketHelper);

            Template.League.LeagueName.leagueName = string.Join(null, new string[] { betTypeName, HtmlCharacters.NoneBreakingSpace, ticket.BetCheck });
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score.Hide();
        }

        protected override string GetBetTeamName(ITicketData ticketData, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetResourceData(GetResourceId(), ticketData.BetTeam);
        }

        protected override List<ITicketData> GetTicketReference(ITicket ticket, List<ITicketData> ticketData)
        {
            var foundTicketData = new List<ITicketData>();

            if (ticketData != null && ticketData.Count > 0)
            {
                foundTicketData = ticketData.FindAll(item => item.RefNo.Equals(ticket.TransId.ToString()));
            }

            return foundTicketData;
        }

        protected virtual string GetResourceId()
        {
            return "GD_Baccarat";
        }
    }
}