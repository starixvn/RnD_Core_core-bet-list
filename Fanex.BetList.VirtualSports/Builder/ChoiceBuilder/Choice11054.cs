namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;
    
    public class Choice11054 : Choice1
    {
        private const string VS4MatchCode = "VYVS_MatchCode";

        protected string _matchRoundInfo;

        protected virtual void GeneralMatchRoundInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo(VS4MatchCode, ticket.TransDesc, ticketHelper);
        }

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            GeneralMatchRoundInfo(ticket, ticketHelper);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected virtual void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var awayTeamName = ticketHelper.GetTeamName3rd(ticket.AwayId.ToString());
            var homeTeamName = ticketHelper.GetTeamName3rd(ticket.HomeId.ToString());

            var isBetHomeTeam = IsBetHomeTeam(ticket);
            Template.betTeam = isBetHomeTeam ? homeTeamName : awayTeamName;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.AppendMatchOrRoundInfo(_matchRoundInfo);
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            rtfHelper.RTFRenderer.AddText($"\n{_matchRoundInfo}");
        }

        protected virtual void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null
                || Template.League.LeagueName == null)
            {
                return;
            }

            Template.League.LeagueName.leagueName = ticketHelper.GetLeagueName3rd(ticket.LeagueId.ToString());
        }
    }
}