namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    public class Choice2001 : Choice1101
    {
        private const string ResourceId = "MCG_LiveCasino";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match.Visible = false;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.Visible = false;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var game = ticketHelper.GetResourceData(ResourceId, ticket.TransDesc);
            var betType = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            game = string.Format("<span class='underdog'>{0}</span>", game);

            Template.League.LeagueName.leagueName = string.Join(null, new string[] { betType, HtmlCharacters.NoneBreakingSpace, game });
        }
    }
}
