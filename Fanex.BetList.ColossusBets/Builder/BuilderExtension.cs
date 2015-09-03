namespace Fanex.BetList.ColossusBets.Builder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;

    public static class BuilderExtension
    {
        public static string BuildColossusBetsTransactionType(this BetListHTMLBuilder factory, ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            if (ticket.Handicap1 > 0)
            {
                return string.Join(string.Empty, new string[] { betTypeName, " ", ticket.Handicap1.ToString(), "%" });
            }

            return betTypeName;
        }

        public static string BuildColossusBetsDetailStatus(this BetListHTMLBuilder factory, ITicket ticket)
        {
            if (!string.IsNullOrWhiteSpace(ticket.Status))
            {
                switch (ticket.Status.ToLower())
                {
                    case BetStatus.Running:
                        return CoreBetList.running;

                    case BetStatus.Reject:
                        return CoreBetList.reject;

                    case BetStatus.Won:
                        return CoreBetList.won;

                    case BetStatus.Lose:
                        return CoreBetList.lose;

                    case BetStatus.Draw:
                        return CoreBetList.draw;

                    case BetStatus.Waiting:
                        return CoreBetList.waiting;

                    case BetStatus.Void:
                        return CoreBetList.voided;

                    case BetStatus.Refund:
                        return CoreBetList.refund;
                }
            }

            return string.Empty;
        }

        public static string BuildColossusBetsDetailDate(this BetListHTMLBuilder factory, ITicket ticket)
        {
            return ticket.TransDate.ToString();
        }
    }
}