namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Globalization;
    using System.Text;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    public class Choice10300 : ChoiceLC
    {
        private string ChoiceRefPrefix => "WE_BC";
        private string TableCodeRefPrefix => "WE_BT";

        public override string GetDisplayChoice(ITicketHelper ticketHelper, string choice)
            => ticketHelper.GetResourceData(ChoiceRefPrefix, choice);

        public override string GetDisplayTableCode(ITicketHelper ticketHelper, string tableCode)
            => ticketHelper.GetResourceData(TableCodeRefPrefix, tableCode);

        public override string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showWELiveCasinoBetDetails(&quot;")
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