namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;
    using Utils;

    public class Choice1011 : Choice1
    {
        protected const string GameGroupRefId = "GG_Game_GG";
        protected const string GameTypeRefId = "GG_Game_GT";
        protected const string GameRefId = "GG_Game";
        protected const string BuyAmountResourceId = "BuyAmount";
        protected const string RemainingAmountResourceId = "RemainingAmount";

        private const string RemainingAmountKey = "RemainingAmount=";
        private const string BuyAmountKey = "BuyAmount=";
        private const string CurrencyAmountBlock = "<div><span class=\"{0}\">{1}</span>: {2} {3}</div>";
        private const string CurrencyAmountClass = "dark-orange";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (!IsSummaryTicket(ticket.BetCheck) && ticket.Status.Equals(BetStatus.Running, StringComparison.OrdinalIgnoreCase))
            {
                Template.betTeam = BuildCoinsInAmount(ticket.MatchCode, ticket.Currency, ticketHelper);
                Template.betTeam += BuildRemainingAmount(ticket.MatchCode, ticket.Currency, ticketHelper);
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        private string BuildRemainingAmount(string additional, string currency, ITicketHelper ticketHelper)
        {
            string remainingAmount;
            var remainingBlock = additional
                .Split('*')
                .FirstOrDefault(x => x.IndexOf(RemainingAmountKey, StringComparison.OrdinalIgnoreCase) == 0);

            if (remainingBlock == null)
            {
                return string.Empty;
            }

            remainingAmount = ConvertByBetType.Stake(remainingBlock.Remove(0, RemainingAmountKey.Length));

            return Cast.AsDecimal(remainingAmount) == 0
                ? string.Empty
                : string.Format(
                    CurrencyAmountBlock,
                    CurrencyAmountClass,
                    ticketHelper.GetResourceData(GameRefId, RemainingAmountResourceId),
                    currency,
                    remainingAmount);
        }

        private string BuildCoinsInAmount(string additional, string currency, ITicketHelper ticketHelper)
        {
            string buyAmount;
            var buyBlock = additional
                .Split('*')
                .FirstOrDefault(x => x.IndexOf(BuyAmountKey, StringComparison.OrdinalIgnoreCase) == 0);

            if (buyBlock == null)
            {
                return string.Empty;
            }

            buyAmount = ConvertByBetType.Stake(buyBlock.Remove(0, BuyAmountKey.Length));

            return string.Format(
                CurrencyAmountBlock,
                CurrencyAmountClass,
                ticketHelper.GetResourceData(GameRefId, BuyAmountResourceId),
                currency,
                buyAmount);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Underdog;
            Template.Handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var gameType = GetBetTypeName(ticket, ticketHelper, ticketData);
            var gameTypeBlock = $"<span class=\"colorHandicap\">{gameType}</span>";

            if (!IsSummaryTicket(ticket.BetCheck))
            {
                gameTypeBlock += "<br />" + ticket.BetCheck;
            }

            Template.BetType.SetValue($"<div class=\"{Underdog}\">{gameTypeBlock}</div>");
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var gameGroup = ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc);
            Template.Match.SetValue($"<div class=\"gamegroupname\">{gameGroup}</div>");
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.SetValue(BuildGlobalGamingDetailsLink(ticket));
            }
            else
            {
                Template.League = null;
            }
        }

        private string BuildGlobalGamingDetailsLink(ITicket ticket)
        {
            var link = string.Format("<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">{0}</a>", CoreBetList.details);

            return string.Format(
                "<div class=\"detail global-gaming-detail\" onclick=\"showGlobalGaming('{0}', '{1}', '{2}', '{3}');\">{4}</div>",
                ticket.TransId.ToString(CultureInfo.InvariantCulture),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(CultureInfo.InvariantCulture),
                GetTransactionType(ticket),
                link);
        }

        private string GetTransactionType(ITicket ticket)
        {
            if (ticket.WinlostDate.Date >= DateTime.Today.AddDays(-1))
            {
                switch (ticket.Status.ToLower())
                {
                    case BetStatus.Won:
                    case BetStatus.Lose:
                    case BetStatus.Draw:
                    case BetStatus.Refund:
                        return "1";

                    case BetStatus.Reject:
                    case BetStatus.Void:
                        return "2";

                    case BetStatus.Running:
                    case BetStatus.Waiting:
                        return "3";
                }
            }

            return "0"; // Previous days Ticket
        }

        public override string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData = null)
        {
            return ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc);
        }

        private bool IsSummaryTicket(string betCheck)
        {
            return string.IsNullOrEmpty(betCheck);
        }

        protected override IRichTextString BuildRTFChoice(ITicket ticket, RTFHelper rtfHelper, ITicketHelper ticketHelper)
        {
            var htmlTagMatching = new Regex("</?[^<>]*>");

            var remainingAmount = ticketHelper.GetResourceData(GameRefId, RemainingAmountResourceId);
            var currencyAmount = htmlTagMatching.Replace(Template.betTeam, string.Empty);
            currencyAmount = currencyAmount.Replace(remainingAmount, "\n" + remainingAmount);
            Template.betTeam = currencyAmount;
            var gameTypeAndPlayId = Template.BetType.ToString().Replace("<br />", "\n");
            gameTypeAndPlayId = htmlTagMatching.Replace(gameTypeAndPlayId, string.Empty);
            Template.BetType.SetValue(gameTypeAndPlayId);
            var gameGroup = htmlTagMatching.Replace(Template.Match.ToString(), string.Empty);
            Template.Match.SetValue(gameGroup);

            rtfHelper.RTFRenderer.AddText(currencyAmount);
            rtfHelper.RTFRenderer.AddText("\n" + gameTypeAndPlayId, rtfHelper.PosFont);
            rtfHelper.RTFRenderer.AddText("\n" + gameGroup);

            var choiceRtf = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return choiceRtf;
        }
    }
}