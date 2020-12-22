namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Core.Entities;
    using Core.Resources;

    /// <summary>
    /// Happy 5 Linked Games: Over/Under
    /// </summary>
    public class Choice3601 : Choice1
    {
        private readonly int numberOfExpressionsOfEachProperty = 2;
        public const string BetChoiceRefId = "Parlay5Choice";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                var betChoicesName = GetBetChoices(ticket, ticketHelper);
                Template.betTeam = string.Join(", ", betChoicesName);
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
            var betChoicesName = new List<string>();
            var contentTransDesc = Split(ticket.TransDesc, '=');
            if (contentTransDesc.Length == numberOfExpressionsOfEachProperty)
            {
                var betChoicesId = contentTransDesc[1].Trim().ToCharArray();
                foreach (var resourceId in betChoicesId)
                {
                    var choiceName = ticketHelper.GetResourceData(BetChoiceRefId, $"{ticket.BetTypeId}_{resourceId.ToString()}");
                    betChoicesName.Add($"[{choiceName}]");
                }
            }

            return betChoicesName;
        }

        private static string[] Split(string value, char separator)
            => value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}