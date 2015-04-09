namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// SystemParlayData data interface.
    /// </summary>
    public interface ISystemParlayData
    {
        IList<ISystemParlayTicket> TicketList { get; set; }

        IList<ISystemParlaySerial> SerialList { get; set; }

        decimal Outstanding { get; set; }
    }
}