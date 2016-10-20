namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    public class Choice225 : Choice221
    {
        private const string Next1SetPieceNo = "1";
        private const string Next1SetPieceYes = "156";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = string.Empty;
            switch (ticket.BetTeam)
            {
                case Next1SetPieceNo:
                    betTeam = CoreBetList.lblNo;
                    break;

                case Next1SetPieceYes:
                    betTeam = CoreBetList.lblYes;
                    break;
            }

            Template.betTeam = betTeam + GetFastMarketsTimeRange(ticket);
        }

        protected string GetFastMarketsTimeRange(ITicket ticket, int interval = 1)
        {
            var start = ticket.Handicap1;
            var end = ticket.Handicap1 + (interval - 1);

            return $" ({start.ToString("00")}:00-{end.ToString("00")}:59)";
        }
    }
}