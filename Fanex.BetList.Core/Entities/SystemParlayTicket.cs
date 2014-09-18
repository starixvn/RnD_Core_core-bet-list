namespace Fanex.BetList.Core.Entities
{
    using System;

    /// <summary>
    /// SystemParlayTicket data class.
    /// </summary>
    public class SystemParlayTicket : ISystemParlayTicket
    {
        public DateTime WinLostDate { get; set; }

        public long TransId { get; set; }

        public long Serial { get; set; }

        public int StatusId { get; set; }

        public string Status { get; set; }

        public decimal MOdds { get; set; }

        public decimal Stake { get; set; }

        public decimal WinLost { get; set; }

        public decimal Comm { get; set; }

        public decimal PlayerComm { get; set; }
    }
}