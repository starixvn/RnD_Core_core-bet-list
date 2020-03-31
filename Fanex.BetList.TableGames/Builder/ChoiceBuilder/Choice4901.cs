namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Choice4901 : Choice1
    {
        private string _choiceId;
        private string _gameId;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                var parsedTransDesc = TransDescParser.ParseTransDesc(ticket.TransDesc);

                _choiceId = parsedTransDesc.Item1;
                _gameId = parsedTransDesc.Item2;
            }

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string tableGamesBetTypeId;

            Template.betTeam = ticketHelper.GetTableGamesBetChoiceName(_choiceId, out tableGamesBetTypeId);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string tableGamesBetTypeId;

            ticketHelper.GetTableGamesBetChoiceName(_choiceId, out tableGamesBetTypeId);
            Template.BetType.betTypeName = ticketHelper.GetTableGamesBetTypeName(tableGamesBetTypeId);
        }

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var gameTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

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
    }
}
