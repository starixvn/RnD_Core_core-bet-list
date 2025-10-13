namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Globalization;
    using System.Text;

    public class Choice2401 : ChoiceLC
    {
        private const string GameTableIdFormat = "<span class=\"gamegroupname\">{0}</span>";

        private const string AllBetRefId = "AllBet_BT";
        
        public override string GetDisplayChoice(ITicketHelper ticketHelper, string choice)
            => ticketHelper.GetResourceData(AllBetRefId, choice);

        public override string GetDisplayTableCode(ITicketHelper ticketHelper, string tableCode)
            => string.Format(GameTableIdFormat, tableCode);

        public override string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showTodayRunningBetsDetails(&quot;")
                .Append(ticket.TransId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.WinlostDate.ToString(CultureInfo.InvariantCulture))
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.CustId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.GetTransactionType())
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }
    }
}