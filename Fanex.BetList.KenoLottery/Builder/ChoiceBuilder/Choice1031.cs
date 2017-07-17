namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Templates;

    public class Choice1031 : Choice1
    {
        private string _choiceId;
        private string _gameId;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                ParseTransDesc(ticket.TransDesc);
            }

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string lotteryBetTypeId;

            Template.betTeam = ticketHelper.GetLotteryBetChoiceName(_choiceId, out lotteryBetTypeId);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string lotteryBetTypeId;

            ticketHelper.GetLotteryBetChoiceName(_choiceId, out lotteryBetTypeId);
            Template.BetType.betTypeName = ticketHelper.GetLotteryBetTypeName(lotteryBetTypeId);
        }

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            string gameTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            Template.League.LeagueName.leagueName = $"{gameTypeName} [{_gameId}]";
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        private void ParseTransDesc(string transDesc)
        {
            const string GameIdIndicator = "betChoice=";
            int lengthOfNeededInfo = transDesc.IndexOf(';');
            string neededInfo = transDesc.Substring(0, lengthOfNeededInfo < 0 ? transDesc.Length : lengthOfNeededInfo);
            string[] parts = neededInfo.Split(',');

            if (parts.Length == 2)
            {
                if (parts[0].StartsWith(GameIdIndicator, StringComparison.InvariantCultureIgnoreCase))
                {
                    _gameId = parts[0].Substring(parts[0].IndexOf('=') + 1).Trim();
                }

                _choiceId = parts[1].Trim();
            }
        }
    }
}