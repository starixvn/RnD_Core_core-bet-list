using Fanex.BetList.Core.Entities;
using Fanex.BetList.Core.Templates;
using Fanex.BetList.Core.Utils.TicketExt;
using System.Collections.Generic;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class StatusRNGExpireDate : Status1
    {
        protected string sportName = string.Empty;

        public virtual int ExpireDateResultLink => 0;

        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsHideRNGResult(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                var externalLink = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}', '{sportName}'";
                Template.StatusResult.betId = externalLink;
            }
        }

        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            sportName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            return base.Render(ticket, ticketHelper, ticketData, isShowVNIP);
        }

        protected virtual bool IsHideRNGResult(ITicket ticket)
            => string.IsNullOrEmpty(ticket.BetCheck) || ticket.IsTicketHide3rdResultLink(ExpireDateResultLink);
    }
}