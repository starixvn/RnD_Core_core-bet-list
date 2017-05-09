namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Entities;
    using Lucky3.Resources;
    using Templates;

    public class Choice1600 : Choice1
    {
        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            BuildChoice(ticket, ticketHelper, ticketData);
            Template.ScoreMapIcon = null;

            return Template;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = "lucky3";
            Template.betTeam = ticket.TransDesc;
            Template.Handicap.handicap = BuildCombinations(ticket);
            Template.Handicap.Odds.SetValue(BuildBets(ticket));
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match.SetValue($"{Lucky3Labels.Number} {ticket.MatchCode}");
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);

            Template.League.Append(BuildDetailsLink(ticket));
        }

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            var htmlTagMatching = new Regex("</?[^<>]*>");
            string choice = Template.betTeam;

            choice += "\n" + Template.Handicap?.handicap;
            choice += "\n" + htmlTagMatching.Replace(Template.Handicap?.Odds?.ToString(), string.Empty);

            if (!string.IsNullOrEmpty(choice))
            {
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            var match = Template.Match?.ToString();

            if (!string.IsNullOrEmpty(match))
            {
                rtfHelper.RTFRenderer.AddText("\n" + match, rtfHelper.PosFont);
            }
        }

        protected string BuildCombinations(ITicket ticket)
        {
            string combinations = $"{ticket.BetId} {(ticket.BetId > 1 ? Lucky3Labels.Combinations : Lucky3Labels.Combination)}";

            return $"{combinations} x {ticket.Currency ?? string.Empty} {ticket.Handicap1}";
        }

        protected string BuildBets(ITicket ticket)
        {
            var bets = $"{ticket.Odds} {(ticket.Odds > 1 ? Lucky3Labels.Bets : Lucky3Labels.Bet)}";

            return $"<div class=\"num-of-bets\">{bets}</div>";
        }

        protected string BuildDetailsLink(ITicket ticket)
        {
            string statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            string status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLower();

            if (statusId == BetStatusId.Draw || status == BetStatus.Draw
                || statusId == BetStatusId.Won || status == BetStatus.Won
                || statusId == BetStatusId.Lost || status == BetStatus.Lose)
            {
                string[] detailsFunctionDivCompositions =
                {
                    $"<div class=\"detail lucky3-detail\" onclick=\"showLucky3Details({ticket.MatchId},{ticket.TransId},{ticket.CustId},{ticket.BetTypeId});\">",
                    $"<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">{CoreBetList.details}</a>",
                    "</div>"
                };

                return string.Join(null, detailsFunctionDivCompositions);
            }

            return string.Empty;
        }
    }
}