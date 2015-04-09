namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// SystemParlayData data class.
    /// </summary>
    public class SystemParlayData : ISystemParlayData
    {
        public IList<ISystemParlayTicket> TicketList { get; set; }

        public IList<ISystemParlaySerial> SerialList { get; set; }

        public decimal Outstanding { get; set; }
    }
}