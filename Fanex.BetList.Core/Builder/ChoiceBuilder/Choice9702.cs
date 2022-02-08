namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    /// <summary>
    /// Home 1st Inns Batsman Milestones
    /// </summary>
    public class Choice9702 : Choice9404
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = $"{ticketHelper.GetTeamNameById(ticket.TransDesc)} - ";
            var overUnder = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? 
                CoreBetList.over : 
                CoreBetList.under;
            Template.betTeam = $"{teamName}{overUnder}";
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.betTeamClassName = Underdog;
        }
    }
}