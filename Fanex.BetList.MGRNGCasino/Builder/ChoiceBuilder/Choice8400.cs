namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Fanex.BetList.Core.App_GlobalResources;

    public class Choice8400 : Choice1
    {
        public const string GameGroundRefId = "MG_RNG_GG";
        public const string GameTypeRefId = "MG_RNG_GT";

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetResourceData(GameTypeRefId, ticket.BetTypeId.ToString())}</span>";

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam += $" {ticket.BetCheck}";
            }
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(GameGroundRefId, ticket.BetTypeId.ToString())}</span>");

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildMGDetailsLink(ticket));
            }
        }

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = Template.betTeam.StripHTML();

        protected override void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            var sportTypeName = Template.League.sportTypeName;
            var gameGroupName = Template.League.LeagueName.ToString().StripHTML();

            if (!string.IsNullOrEmpty(sportTypeName) && !string.IsNullOrEmpty(gameGroupName))
            {
                rtfHelper.RTFRenderer.AddText($"\n{sportTypeName}{gameGroupName}");
            }
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
           => Template.Match = null;

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;

        private static string BuildMGDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail mgrngcasino-detail\" onclick=\"showMGRNGCasinoBetDetails({parameterList});\">{detailLink}</div>";
        }

        private static bool IsDetailTicket(string betCheck)
                => !string.IsNullOrEmpty(betCheck);

        private static bool IsSummaryTicket(string betCheck)
            => !IsDetailTicket(betCheck);
    }
}
