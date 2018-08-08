namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    public class Choice1402 : Choice1401
    {
        protected override string BuildSeatAndTableBlock(ITicket ticket, ITicketHelper ticketHelper)
        {
            var seatInfo = BuildSeatInfo(ticket, ticketHelper);
            var tableInfo = BuildTableInfo(ticketHelper);
            var blindsInfo = BuildBlindsInfo(ticket, ticketHelper);

            return $"{seatInfo}, {tableInfo} {blindsInfo}";
        }

        protected static string BuildBlindsInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            const string blindsResourceId = "Blinds";

            var blindsPattern = ticketHelper.GetResourceData(GameInfoRefId, blindsResourceId);
            var smallBlind = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1);
            var bigBlind = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1 * 2);

            return string.Format(blindsPattern, smallBlind, bigBlind);
        }
    }
}