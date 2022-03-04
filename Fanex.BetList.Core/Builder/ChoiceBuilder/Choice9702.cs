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
            var teamName = ticket.TransDesc != null ? $"{ticketHelper.GetTeamNameById(ticket.TransDesc)} - " : string.Empty;
            var overUnder = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? 
                $"<span class=\"favorite\">{CoreBetList.over}</span>" : 
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