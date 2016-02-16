namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Templates;

    public class Choice2101 : Choice1
    {
        private const string BetTypeOpenTag = "<span class=\"colorHandicap\">";
        private const string BetTypeCloseTag = "</span> ";
        private const string GameTypeRefId = "RNG_Casino_GT";
        private const string GameGroupRefId = "RNG_Casino_GG";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betChoice = ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc);

            if (!string.IsNullOrEmpty(betChoice))
            {
                betChoice = BetTypeOpenTag + betChoice + BetTypeCloseTag;
            }

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                betChoice += ticket.BetCheck;
            }

            Template.betTeam = betChoice;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null || Template.League.LeagueName == null)
            {
                return;
            }

            Template.League.LeagueName.leagueName = ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc);

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.League.Append(BuildShowRnGBetsDetails(ticket));
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            base.AdjustBetTeamToRTF();

            Template.betTeam = Template.betTeam.Replace(BetTypeOpenTag, string.Empty);
            Template.betTeam = Template.betTeam.Replace(BetTypeCloseTag, string.Empty);
            Template.betTeam = Template.betTeam.Trim();
        }

        private Choice_League_Block BuildShowRnGBetsDetails(ITicket ticket)
        {
            string[] detailsFunctionDivCompositions = new string[]
            {
                "<div class=\"detail rng-casino-detail\" onclick=\"showRnGCasinoDetails(",
                ticket.TransId.ToString(CultureInfo.InvariantCulture),
                ", ",
                "'",
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                "'",
                ", ",
                ticket.CustId.ToString(CultureInfo.InvariantCulture),
                ", ",
                GetTransactionType(ticket),
                ");\">",
                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">",
                CoreBetList.details,
                "</a>",
                "</div>"
            };
            string detailLinkMarkup = string.Join(null, detailsFunctionDivCompositions);
            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(detailLinkMarkup);

            return detailtTemplateBlock;
        }

        private string GetTransactionType(ITicket ticket)
        {
            // @TransType
            // + 0 : All (DRAW/WON/LOSE/Reject/Void/Refund/Running/Waiting)
            // + 1 : DRAW/WON/LOSE/Refund
            // + 2 : Reject/Void
            // + 3 : Running/Waiting
            // Use this because web server and db are in the same timezone
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
    }
}