namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Utils.Parsers;    
    using Fanex.BetList.Core.Entities;
    using System;

    public class Status3101 : StatusLC
    {
        private const int SupportedResultDays = -14;

        protected override void BuildStatusResult(ITicket ticket)
        {
            if (IsHideStatusResult(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                var items = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
                var site = items.ContainsKey("site") ? items["site"] : string.Empty;

                var externalLink = $"{ticket.CurrencyId}, '{site}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
                Template.StatusResult.betId = externalLink;
            }
        }

        public override bool IsHideStatusResult(ITicket ticket)
        => base.IsHideStatusResult(ticket) || !IsSupportViewResultLink(ticket);

        private static bool IsSupportViewResultLink(ITicket ticket)
        {
            var minDate = DateTime.Today.AddDays(SupportedResultDays);

            return ticket.WinlostDate >= minDate;
        }
    }
}