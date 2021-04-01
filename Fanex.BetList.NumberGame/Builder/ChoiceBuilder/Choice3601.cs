namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Core.Entities;
    using Core.Resources;
    using Fanex.BetList.Core.Utils.StringExt;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Happy 5 Linked Games: Over/Under
    /// </summary>
    public class Choice3601 : Choice1
    {
        private readonly int numberOfExpressionsOfEachProperty = 2;
        public const string BetChoiceRefId = "Parlay5Choice";
        private const string Hyphen = "-";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                var betChoices = GetBetChoices(ticket, ticketHelper);
                var totalBalls = betChoices.Count(i => !i.Equals($"[{Hyphen}]"));
                var totalBallKeyResourceId = $"{ticket.SportTypeId}_{totalBalls}";
                var totalBallsFormat = $"<div class='underdog'>{ticketHelper.GetResourceData(BetChoiceRefId, totalBallKeyResourceId)}</div>";

                Template.betTeam = totalBallsFormat + string.Join(", ", betChoices);
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var matchCodeText = GetMatchCode(ticket.BetCheck);

            Template.Match.SetValue($"<div class=\"match\">{matchCodeText}</div>");
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap = null;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.LeagueName.leagueName = string.Empty;

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            var matchCodeText = GetMatchCode(ticket.BetCheck);

            rtfHelper.RTFRenderer.AddText("\n" + matchCodeText);
        }

        private static string GetMatchCode(string betCheck)
        {
            string[] matchEmpty = { string.Empty, string.Empty };
            var matchCodes = string.IsNullOrEmpty(betCheck) ? matchEmpty : betCheck.Split(';');
            matchCodes = matchCodes[0].Split('-');
            var matchCode = matchCodes.Length >= 3 ? matchCodes[2] : string.Empty;

            return string.Format(NumberGameLang.Number, matchCode);
        }

        private List<string> GetBetChoices(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betChoices = new List<string>();
            var contentTransDesc = Split(ticket.TransDesc, '=');
            if (contentTransDesc.Length == numberOfExpressionsOfEachProperty)
            {
                var betChoicesId = contentTransDesc[1].Trim().ToCharArray();
                foreach (var id in betChoicesId)
                {
                    var resourceId = id.ToString();
                    if (resourceId.Equals(Hyphen))
                    {
                        betChoices.Add($"[{Hyphen}]");
                    }
                    else
                    {
                        var choiceName = ticketHelper.GetResourceData(BetChoiceRefId, $"{ticket.BetTypeId}_{resourceId}");
                        betChoices.Add($"[{choiceName}]");
                    }
                }
            }

            return betChoices;
        }

        private static string[] Split(string value, char separator)
            => value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}