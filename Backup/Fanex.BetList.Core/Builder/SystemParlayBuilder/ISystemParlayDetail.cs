namespace Fanex.BetList.Core.Builder.SystemParlayBuilder
{
    using Entities;

    /// <summary>
    /// Bet Type: Handicap.
    /// </summary>
    public interface ISystemParlayDetail
    {
        string Build(ITicketHelper ticketHelper, ISystemParlayData systemParlayData);
    }
}