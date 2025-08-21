namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.Parsers;

    public class Status3300 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (ticket.IsSummaryTicket() || !IsSupportViewResultLink(ticket))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                var transDescItems = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
                var gameId3rd = transDescItems.ContainsKey("gamename") ? transDescItems["gamename"] : string.Empty;
                var externalLink = string.Format("{0}, '{1}', {2}, '{3}', '{4}'",
                    ticket.CurrencyId,
                    gameId3rd,
                    ticket.CustId,
                    ticket.BetCheck,
                    ticket.SiteId);
                Template.StatusResult.betId = externalLink;
            }
        }

        private static bool IsSupportViewResultLink(ITicket ticket)
        {
            const int dayAgoSupportResultPage = -30;
            var minDate = DateTime.Today.AddDays(dayAgoSupportResultPage);

            return ticket.WinlostDate >= minDate && !ticket.IsSummaryTicket();
        }
    }
}
