namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Home 1st Inns Batsman Milestones
    /// </summary>
    public class Choice9715 : Choice9541
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = $"<span class='{Underdog}'>{ticketHelper.GetTeamNameById(ticket.TransDesc)}</span> - ";
            var yesNo = BetTeamValue.N == ticket.BetTeam.ToLowerInvariant() ? 
                CoreBetList.lblNo : 
                CoreBetList.lblYes;
            Template.betTeam = $"{teamName}{yesNo}";
        }
    }
}