namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Choice5001 : Choice1
    {
        private MemberChoice choiceInfo;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            choiceInfo = GetBetChoiceId(ticket.TransDesc);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = choiceInfo.BetContent;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.LeagueName.leagueName = $"{choiceInfo.DrawType} {choiceInfo.DrawDate}";

        private static MemberChoice GetBetChoiceId(string transDesc)
        {
            var choice = new MemberChoice();
            if (!string.IsNullOrEmpty(transDesc))
            {
                var separators = new[] { ';', '|' };
                var choiceItems = transDesc.Split(separators);
                const int numberElementOfChoice = 3;
                const int betContentIndex = 2;

                if (choiceItems.Length == numberElementOfChoice)
                {
                    choice.DrawType = choiceItems[0];
                    choice.DrawDate = choiceItems[1];
                    choice.BetContent = choiceItems[betContentIndex];
                }
            }

            return choice;
        }

        private class MemberChoice
        {
            public string DrawType { get; set; }

            public string DrawDate { get; set; }

            public string BetContent { get; set; }
        }
    }
}