namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice2001 : Choice1101
    {
        private const string OpenBetTypeNameTag = "<span class='micro-gaming-bet-type'>";
        private const string CloseBetTypeNameTag = "</span>";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betType = BuildBetTypeName(ticket.BetTypeId, ticketHelper);

            Template.League.LeagueName.leagueName = betType;
        }

        private string BuildBetTypeName(int betTypeId, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(betTypeId);

            return string.Format("{0}{1}{2}", OpenBetTypeNameTag, betTypeName, CloseBetTypeNameTag);
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.League.LeagueName.leagueName = Template.League.LeagueName.leagueName
                .Replace(OpenBetTypeNameTag, string.Empty)
                .Replace(CloseBetTypeNameTag, string.Empty);
        }
    }
}