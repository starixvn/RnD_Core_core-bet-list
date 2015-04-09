namespace Fanex.BetList.Core.Builder.StatusBuilder._3rd
{
    using Entities;

    /// <summary>
    /// Build Status for PlayTech Casino.
    /// </summary>
    public class Status_22006 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            Template.StatusResult = null;
        }

        protected override void BuildIP(ITicket ticket, ITicketHelper ticketHelper, bool isShowVNIP)
        {
            Template.ShowIP = null;
        }
    }
}