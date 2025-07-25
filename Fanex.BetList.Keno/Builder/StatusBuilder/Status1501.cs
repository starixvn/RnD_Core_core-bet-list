namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Collections.Generic;

    public class Status1501 : Status1
    {
        private string sportName = string.Empty;
        public virtual int ExpireDateResultLink => 14;

        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            sportName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            return base.Render(ticket, ticketHelper, ticketData, isShowVNIP);
        }

        protected override void BuildStatusResult(ITicket ticket)
        {
            if (ticket.IsTicketHide3rdResultLink(ExpireDateResultLink))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}', '{sportName}'";
            }
        }
    }
}