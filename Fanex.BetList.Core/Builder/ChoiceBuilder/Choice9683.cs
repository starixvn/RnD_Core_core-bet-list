namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    /// <summary>
    ///  Home 1st Inns Batsman Runs OE
    /// </summary>
    public class Choice9683 : Choice2
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = $"{ticketHelper.GetTeamNameById(ticket.TransDesc)} - ";
            var oddEven = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ?
                $"<span class=\"favorite\">{CoreBetList.odd}</span>" : 
                CoreBetList.even;
            Template.betTeam = $"{teamName}{oddEven}";
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}