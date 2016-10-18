namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Core.Entities;
    using NPOI.SS.UserModel;

    public class Choice1011 : Choice1
    {
        protected const string GameGroupRefId = "GG_Game_GG";
        protected const string GameTypeRefId = "GG_Game_GT";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var gameType = GetBetTypeName(ticket, ticketHelper, ticketData);
            Template.betTeam = $"<span class='colorHandicap'>{gameType}</span>";

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.betTeam += " " + ticket.BetCheck;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Underdog;
            Template.Handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var gameGroup = ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc);
            Template.Match.SetValue($"<div class='gamegroupname'>{gameGroup}</div>");
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (string.IsNullOrEmpty(ticket.BetCheck))
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

        protected override IRichTextString BuildRTFChoice(ITicket ticket, RTFHelper rtfHelper, ITicketHelper ticketHelper)
        {
            var htmlTagMatching = new Regex("</?[^<>]*>");

            var gameTypeAndPlayId = htmlTagMatching.Replace(Template.betTeam, string.Empty);
            var gameGroup = htmlTagMatching.Replace(Template.Match.ToString(), string.Empty);

            rtfHelper.RTFRenderer.AddText(gameTypeAndPlayId, rtfHelper.PosFont);
            rtfHelper.RTFRenderer.AddText("\n" + gameGroup);

            var choiceRtf = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return choiceRtf;
        }
    }
}