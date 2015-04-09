namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name: Player Sent Off.
    /// </summary>
    public class Choice210 : Choice133
    {
        protected override void BuildScore(ITicket ticket)
        {
            Template.Score.homeScore = ticket.LiveHomeScore.ToString();
            Template.Score.awayScore = ticket.LiveAwayScore.ToString();
            Template.Score.Visible = ticket.IsLive;
            Template.Score.scoreClassName = Favorite;
        }
    }
}