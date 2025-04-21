namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Collections.Generic;

    public class StatusLCExpireDate : Status1
    {
        private string sportName = string.Empty;

        public virtual int ExpireDateResultLink => 0;

        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsHideStatusResult(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                var externalLink = $"{ticket.CurrencyId}, '{ticket.MatchCode}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}', '{sportName}'";
                Template.StatusResult.betId = externalLink;
            }
        }

        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            sportName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            return base.Render(ticket, ticketHelper, ticketData, isShowVNIP);
        }

        private bool IsHideStatusResult(ITicket ticket)
            => string.IsNullOrEmpty(ticket.BetCheck) || ticket.IsTicketHide3rdResultLink(ExpireDateResultLink);
    }
}