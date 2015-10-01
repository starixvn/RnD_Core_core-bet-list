namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils;

    public class Choice301 : Choice1
    {
        private readonly string _percentageFormat = " <span class='{0}'>({1})</span>";

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.Odds = null;

            this.BuildOddsSpread(ticket);
        }

        protected void BuildOddsSpread(ITicket ticket)
        {
            var percentageClass = ticket.OddsSpread < 0 ? Favorite : string.Empty;
            var percentageValue = Formatter.DecFormat(ticket.OddsSpread, 0);
            Template.Handicap.handicap += string.Format(_percentageFormat, percentageClass, percentageValue);
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