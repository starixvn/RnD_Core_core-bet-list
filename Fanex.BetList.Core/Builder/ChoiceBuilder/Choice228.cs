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

            var timeNumber = Convert.ToInt32(ticket.Handicap1) + 1;
            var timeString = Formatter.FormatTwoDigit(timeNumber);
            var timeMachine = $"<span class='favorite'> (00:00 - {timeString}:00)</span>";

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.OddsSpread) + timeMachine;
            Template.betTeamClassName = IsOver(ticket) ? Favorite : Underdog;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = IsOver(ticket) ? CoreBetList.over : CoreBetList.under;

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            Template.Handicap.handicap = Template.Handicap.handicap.Replace("<span class='favorite'>", string.Empty);
            Template.Handicap.handicap = Template.Handicap.handicap.Replace("</span>", string.Empty);

            base.BuildBetFigure(ticket, ref rtfHelper, isMixParlay);            
        }
        private static bool IsOver(ITicket ticket) => ticket.BetTeam.ToLowerInvariant() == BetTeamValue.O;
    }
}