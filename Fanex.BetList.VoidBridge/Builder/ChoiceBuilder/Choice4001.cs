namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;
    using Templates;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Choice4001 : Choice1
    {
        private const string GameGroupRefId = "VB_Casino_GG";
        private const string GameTypeRefId = "VB_Casino_GT";

        private const string OpenSportTypeNameTag = "<span class='voidbridge-sporttypename'>";
        private const string CloseSportTypeNameTag = "</span>";
        private const string OpenGameGroupNameTag = "<span class='voidbridge-gamegroupname'>";
        private const string CloseGameGroupNameTag = "</span>";
        private const string OpenGameTypeNameTag = "<span class='voidbridge-gametypename'>";
        private const string CloseGameTypeNameTag = "</span>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (Template.betTeam == null)
            {
                return;
            }

            Template.betTeam = string.Format(
                "{0}{1}{2}",
                OpenGameTypeNameTag,
                ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc),
                CloseGameTypeNameTag);

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.betTeam += " " + ticket.BetCheck;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null)
            {
                return;
            }

            Template.League.sportTypeName = string.Format(
                "{0}{1}{2}",
                OpenSportTypeNameTag,
                ticketHelper.GetSportNameById(ticket.SportTypeId),
                CloseSportTypeNameTag);
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null || Template.League.LeagueName == null)
            {
                return;
            }

            Template.League.LeagueName.leagueName = string.Format(
                "{0}{1}{2}",
                OpenGameGroupNameTag,
                ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc),
                CloseGameGroupNameTag);

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.League.Append(BuildVoidBridgeDetailsLink(ticket));
            }
        }

        private Choice_League_Block BuildVoidBridgeDetailsLink(ITicket ticket)
        {
            string[] detailsFunctionDivCompositions = new string[]
            {
                "<div class=\"detail voidbridge-detail\" onclick=\"showVoidBridgeDetails(",
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

        protected override void AdjustBetTeamToRTF()
        {
            Template.League.sportTypeName = Template.League.sportTypeName
                .Replace(OpenSportTypeNameTag, string.Empty)
                .Replace(CloseSportTypeNameTag, string.Empty);

            Template.League.LeagueName.leagueName = Template.League.LeagueName.leagueName
                .Replace(OpenGameGroupNameTag, string.Empty)
                .Replace(CloseSportTypeNameTag, string.Empty);

            Template.betTeam = Template.betTeam
                .Replace(OpenGameTypeNameTag, string.Empty)
                .Replace(CloseSportTypeNameTag, string.Empty);
        }
    }
}