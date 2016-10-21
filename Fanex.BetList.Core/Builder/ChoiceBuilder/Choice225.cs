namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    public class Choice225 : Choice4
    {
        private const string Next1MinuteSetPieceNo = "1";
        private const string Next1MinuteSetPieceYes = "156";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = string.Empty;
            switch (ticket.BetTeam)
            {
                case Next1MinuteSetPieceNo:
                    betTeam = CoreBetList.lblNo;
                    break;

                case Next1MinuteSetPieceYes:
                    betTeam = CoreBetList.lblYes;
                    break;
            }

            Template.betTeam = betTeam + GetFastMarketsTimeRange(ticket);
        }

        protected string GetFastMarketsTimeRange(ITicket ticket, int minutesTimeSpan = 1)
        {
            var start = ticket.Handicap1;
            var end = ticket.Handicap1 + (minutesTimeSpan - 1);

            return $" ({start.ToString("00")}:00-{end.ToString("00")}:59)";
        }
    }
}