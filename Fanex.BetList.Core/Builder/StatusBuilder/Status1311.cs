namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// Set x Winner.
    /// </summary>
    public class Status1311 : Status1
    {
        /// <summary>
        /// Builds the status result.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var refNo = "0";
            if (ticket.BetId.ToString().Length > 2)
            {
                refNo = ticket.BetId.ToString().Substring(1, 2);
            }

            Template.StatusResult.refNo = refNo;
        }
    }
}