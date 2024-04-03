namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Templates;

    public class Choice2101 : ChoiceRNG
    {
        private const string BetTypeOpenTag = "<span class=\"colorHandicap\">";
        private const string BetTypeCloseTag = "</span> ";
        private const string GameTypeRefId = "RNG_Casino_GT";
        private const string GameGroupRefId = "RNG_Casino_GG";
        private const string GameGroupNameArea = "<span class='gamegroupname'> {0}</span>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betChoice = ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc);

            if (!string.IsNullOrEmpty(betChoice))
            {
                betChoice = BetTypeOpenTag + betChoice + BetTypeCloseTag;
            }

            if (IsDetailTicket(ticket.BetCheck))
            {
                betChoice += GetTransId3rd(ticket.BetCheck);
            }

            Template.betTeam = betChoice;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null || Template.League.LeagueName == null)
            {
                return;
            }

            var gameGroupName = ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc);
            Template.League.LeagueName.SetValue(string.Format(GameGroupNameArea, gameGroupName));

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildDetailsLink(ticket));
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            base.AdjustBetTeamToRTF();
            
            Template.betTeam = Template.betTeam.Trim();
        }

        protected override string BuildDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail rng-casino-detail\" onclick=\"showRnGCasinoDetails({parameterList});\">{detailLink}</div>";
        }

        protected override void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            if (Template.League != null)
            {
                var htmlTagMatching = new Regex("</?[^<>]*>");
                string choice = Template.League.sportTypeName;

                choice += Template.League.LeagueName;
                choice = htmlTagMatching.Replace(choice, string.Empty);

                Template.League.SetValue(choice);

                if (!string.IsNullOrEmpty(choice))
                {
                    choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, string.Empty);
                    rtfHelper.RTFRenderer.AddText("\n" + choice);
                }
            }
        }
    }
}