namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Resource;
    using Fanex.BetList.Core.Templates;

    public class Choice5001 : Choice1
    {
        private const string betOptionOpenTag = "<div class=\"underdog\">";
        private const string betOptionCloseTag = "</div>";
        private MemberChoice choiceInfo;

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            choiceInfo = GetBetChoiceId(ticket.TransDesc);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = choiceInfo.BetContent;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var optionText = string.Join(" / ", choiceInfo.Options);
            var option = choiceInfo.Options.Count > 0 ? $"{betOptionOpenTag}{optionText}{betOptionCloseTag}" : string.Empty;
            Template.Handicap.handicap = option;
            Template.betTeamClassName = Favorite;
        }

        protected override void AdjustBetTeamToRTF()
            => Template.Handicap.handicap = Template.Handicap.handicap.Replace(betOptionOpenTag, "\n").Replace(betOptionCloseTag, string.Empty);

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
                const int boxIndex = 3;
                const int luckyPickIndex = 4;

                if (choiceItems.Length >= numberElementOfChoice)
                {
                    choice.DrawType = choiceItems[0];
                    choice.DrawDate = choiceItems[1];
                    choice.BetContent = choiceItems[betContentIndex];

                    if (choiceItems.Length > luckyPickIndex && !string.IsNullOrWhiteSpace(choiceItems[luckyPickIndex]))
                    {
                        choice.Options.Add(Indo4DResources.LuckyPick);
                    }
                    if (choiceItems.Length > boxIndex && !string.IsNullOrWhiteSpace(choiceItems[boxIndex]))
                    {
                        choice.Options.Add(Indo4DResources.Box);
                    }
                }
            }

            return choice;
        }

        private sealed class MemberChoice
        {
            public MemberChoice()
            {
                Options = new List<string>();
            }

            public string DrawType { get; set; }

            public string DrawDate { get; set; }

            public string BetContent { get; set; }

            public List<string> Options { get; set; }
        }
    }
}