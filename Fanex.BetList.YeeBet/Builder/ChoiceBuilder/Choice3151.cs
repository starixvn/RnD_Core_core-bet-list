namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Globalization;
    using System.Text;

    public class Choice3151 : ChoiceLC
    {
        public const string YeeBetRefId = "YB_BT";

        public override string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showYeeBetBetDetails(&quot;")
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

        public override string GetDisplayChoice(ITicketHelper ticketHelper, string choice)
            => ticketHelper.GetResourceData(YeeBetRefId, choice);

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());
            base.AdjustBetTeamToRTF();
        }

        protected override bool IsDetailTicket(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.BetCheck);
    }
}