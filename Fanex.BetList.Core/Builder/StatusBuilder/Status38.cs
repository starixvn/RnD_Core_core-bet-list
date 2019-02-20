namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using Entities;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Single Match Parlay
    /// </summary>
    public class Status38 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.betId = ticket.BetCheck;
        }
        
        protected override bool DetectAbnormalBet(ITicket ticket) => false;
    }
}