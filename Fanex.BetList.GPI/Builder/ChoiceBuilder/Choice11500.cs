namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Globalization;
    using System.Collections.Generic;
    using System.Text;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    public class Choice11500 : ChoiceLC
    {
        public const string ChoiceRefPrefix = "GPIL_BT";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = IsDetailTicket(ticket) ?
            ticketHelper.GetResourceData(ChoiceRefPrefix, ticket.BetTypeId.ToString()) :
            null;
        }

        public override string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showGPILCBetDetails(&quot;")
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
        protected override bool IsDetailTicket(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.BetCheck);

    }
}