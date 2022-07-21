namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Templates;

    public class Choice4001 : Choice1
    {
        private const string GameGroupRefId = "VB_Casino_GG";
        private const string GameTypeRefId = "VB_Casino_GT";

        private const string OpenSportTypeNameTag = "<span class='voidbridge-sporttypename'>";
        private const string CloseSportTypeNameTag = "</span>";
        private const string GameGroupNameArea = "<span class='gamegroupname'>{0}</span>";
        private const string OpenGameTypeNameTag = "<span class='colorHandicap'>";
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
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
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
                GameGroupNameArea,
                ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc));

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.League.Append(BuildVoidBridgeDetailsLink(ticket));
            }
        }

        private static Choice_League_Block BuildVoidBridgeDetailsLink(ITicket ticket)
        {
            string[] detailsFunctionDivCompositions =
            {
                "<div class=\"detail voidbridge-detail\" onclick=\"showVoidBridgeDetails('",
                ticket.TransId.ToString(CultureInfo.InvariantCulture),
                "', ",
                "'",
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                "'",
                ", ",
                ticket.CustId.ToString(CultureInfo.InvariantCulture),
                ", ",
                ticket.GetTransactionType(),
                ");\">",
                "<a class=\"detail-link\" href=\"javascript:void('');\">",
                CoreBetList.details,
                "</a>",
                "</div>"
            };
            string detailLinkMarkup = string.Join(null, detailsFunctionDivCompositions);
            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(detailLinkMarkup);

            return detailtTemplateBlock;
        }

        protected override void AdjustBetTeamToRTF()
        {
            var htmlTagMatching = new Regex("</?[^<>]*>");

            Template.League.sportTypeName = Template.League.sportTypeName
                .Replace(OpenSportTypeNameTag, string.Empty)
                .Replace(CloseSportTypeNameTag, string.Empty);

            Template.League.LeagueName.leagueName = htmlTagMatching.Replace(Template.League.LeagueName.leagueName, string.Empty);

            Template.betTeam = Template.betTeam
                .Replace(OpenGameTypeNameTag, string.Empty)
                .Replace(CloseSportTypeNameTag, string.Empty);
        }
    }
}