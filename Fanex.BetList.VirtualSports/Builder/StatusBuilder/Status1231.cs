namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// Virtual Horse Racing - Win.
    /// </summary>
    public class Status1231 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.race = string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race;
        }

        protected override string GetHalfWonLost(string statusText, ITicket ticket)
        {            
            var status = ticket.Status.ToLowerInvariant();
            if (status == BetStatus.Won && ticket.MasterOdds == CommonValue.HalfWonLostValue)
            {
                return CoreBetList.halfwon;
            }
            else if (status == BetStatus.Lose && ticket.MasterOdds == -1 * CommonValue.HalfWonLostValue)
            {
                return CoreBetList.halflose;
            }
            else
            {
                return statusText;
            }
        }
    }
}