namespace Fanex.BetList.Core.Entities
{
    using System;

    /// <summary>
    /// Ticket Data.
    /// </summary>
    [Serializable]
    public class TicketData : ITicketData
    {
        #region ITicketData Members

        public string RefNo { get; set; }

        public int BetTypeId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// Values: Running, Waiting, Refund, Void, Won, Lose, Draw, Reject.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        public decimal Odds { get; set; }

        public decimal Stake { get; set; }

        public string BetTeam { get; set; }

        public string TransDesc { get; set; }

        public decimal SuperPT { get; set; }

        public decimal MasterPT { get; set; }

        public decimal AgentPT { get; set; }

        #endregion ITicketData Members
    }
}