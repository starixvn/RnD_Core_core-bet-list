namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    /// <summary>Microgaming Live Casino Choice 13900.</summary>
    public class Choice13900 : ChoiceLC
    {
        public const string ChoiceRefPrefix = "MGL_BT";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = IsDetailTicket(ticket) ? ticketHelper.GetResourceData(ChoiceRefPrefix, ticket.BetTypeId.ToString()) : null;

        public override string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showMicroGamingLCBetDetails(&quot;")
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

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = IsDetailTicket(ticket) ? $"{OpenBetIdTag}{CoreBetList.BetID}: {ticket.TransDesc}{CloseBetIdTag}" : string.Empty;

    }
}