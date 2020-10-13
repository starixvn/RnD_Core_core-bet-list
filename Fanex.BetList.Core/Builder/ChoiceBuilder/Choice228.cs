namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    public class Choice228 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            var timeNumber = Convert.ToInt32(ticket.Handicap1);
            var timeMachine = $"<span class='favorite'> ({CoreBetList.Until} {timeNumber.ToString("D2")}:59)</span>";

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.OddsSpread) + timeMachine;
            Template.betTeamClassName = IsOver(ticket) ? Favorite : Underdog;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = IsOver(ticket) ? CoreBetList.over : CoreBetList.under;

        private static bool IsOver(ITicket ticket) => ticket.BetTeam.ToLowerInvariant() == BetTeamValue.O;
    }
}