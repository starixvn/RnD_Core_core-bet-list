namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Templates;
    using Utils;

    public delegate string MemberChoiceMappingHandler(ITicket ticket, ITicketHelper ticketHelper);

    public class Choice1501 : Choice1
    {
        private static Dictionary<string, MemberChoiceMappingHandler> memberChoiceMapper = new Dictionary<string, MemberChoiceMappingHandler>
        {
            ["33"] = (ticket, ticketHelper) => BuildIndexBigSmallBetTeam("35", ticket, ticketHelper),
            ["34"] = (ticket, ticketHelper) => BuildIndexBigSmallBetTeam("36", ticket, ticketHelper)
        };

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
            string memberChoice;

            if (!string.IsNullOrEmpty(_choiceId) && memberChoiceMapper.ContainsKey(_choiceId))
            {
                memberChoice = memberChoiceMapper[_choiceId](ticket, ticketHelper);
            }
            else
            {
                var betChoiceInfo = ticketHelper.GetKenoBetChoiceInfo(_choiceId);
                memberChoice = betChoiceInfo != null ? betChoiceInfo.ChoiceName : string.Empty;
            }

            Template.betTeam = memberChoice;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betChoiceInfo = ticketHelper.GetKenoBetChoiceInfo(_choiceId);
            var bettypeId = betChoiceInfo != null ? betChoiceInfo.BetTypeId : string.Empty;
            Template.BetType.betTypeName = ticketHelper.GetKenoBetTypeName(bettypeId);
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
            // Does not show handicap, match, and score.
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        private static string BuildIndexBigSmallBetTeam(string mappedChoiceId, ITicket ticket, ITicketHelper ticketHelper)
        {
            var betChoiceInfo = ticketHelper.GetKenoBetChoiceInfo(mappedChoiceId);
            var memberChoicePattern = betChoiceInfo != null ? betChoiceInfo.ChoiceName : string.Empty;
            decimal bets = 0;
            int range = 0;

            if (!string.IsNullOrEmpty(ticket.BetCheck) && int.TryParse(ticket.BetCheck, out range) && range != 0)
            {
                bets = ticket.Stake / range;
            }

            return string.Format(memberChoicePattern, ConvertByBetType.Stake(bets), range);
        }

        private void ParseTransDesc(string transDesc)
        {
            const string gameIdIndicator = "betChoice=";
            const int numberOfExpectedInfo = 2;

            int lengthOfNeededInfo = transDesc.IndexOf(';');
            string neededInfo = transDesc.Substring(0, lengthOfNeededInfo < 0 ? transDesc.Length : lengthOfNeededInfo);
            string[] parts = neededInfo.Split(',');

            if (parts.Length == numberOfExpectedInfo)
            {
                if (parts[0].StartsWith(gameIdIndicator, StringComparison.InvariantCultureIgnoreCase))
                {
                    _gameId = parts[0].Substring(parts[0].IndexOf('=') + 1).Trim();
                }

                _choiceId = parts[1].Trim();
            }
        }
    }
}