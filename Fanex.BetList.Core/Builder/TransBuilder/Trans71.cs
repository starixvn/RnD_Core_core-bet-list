namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;

    /// <summary>
    /// Casino Games.
    /// </summary>
    public class Trans71 : Trans1
    {
        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }
    }
}