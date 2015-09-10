namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    public class Choice301 : Choice1
    {
        private const string PercentageFormat = " <span class='{0}'>({1})</span>";

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.Odds = null;
            Template.Handicap.handicap += string.Format(PercentageFormat, ticket.OddsSpread > 0 ? Favorite : string.Empty, ticket.OddsSpread);
        }

        protected override void AdjustBetTeamToRTF()
        {
            base.AdjustBetTeamToRTF();

            Template.Handicap.handicap = Template.Handicap.handicap.Replace("<span class='favorite'>", string.Empty);
            Template.Handicap.handicap = Template.Handicap.handicap.Replace("<span class=''>", string.Empty);
            Template.Handicap.handicap = Template.Handicap.handicap.Replace("</span>", string.Empty);
        }
    }
}
