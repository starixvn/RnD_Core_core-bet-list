namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status5401 : StatusRNG
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            
            var transDate3rd = Formatter.FullDateFormat(ticket.EventDate.Value);
            Template.StatusResult.winlostDate = transDate3rd;
        }

        protected override bool IsHideRNGResult(ITicket ticket)
        {
            const int daySupportResultLink = 7;

            return string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}