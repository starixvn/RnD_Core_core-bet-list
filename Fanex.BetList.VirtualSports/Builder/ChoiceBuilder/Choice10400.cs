namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;
    
    public class Choice10400 : Choice1
    {
        private const string VS4MatchCode = "EGVS_MatchCode";

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

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.AppendMatchOrRoundInfo(_matchRoundInfo);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            rtfHelper.RTFRenderer.AddText($"\n{_matchRoundInfo}");
        }
    }
}