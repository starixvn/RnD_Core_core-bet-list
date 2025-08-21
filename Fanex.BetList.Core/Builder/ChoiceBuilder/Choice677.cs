namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    public class Choice677 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = ticket.TransDesc != null ? $"{ticketHelper.GetTeamNameById(ticket.TransDesc)} - " : string.Empty;
            var yesNo = BetTeamValue.N == ticket.BetTeam.ToLowerInvariant() ? 
                CoreBetList.lblNo : 
                CoreBetList.lblYes;
            Template.betTeam = $"{teamName}<span class=\"favorite\">{yesNo}</span>";
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}