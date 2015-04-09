namespace Fanex.BetList.Core.Entities
{
    using System;

    /// <summary>
    /// SystemParlayTicket data interface.
    /// </summary>
    public interface ISystemParlayTicket
    {
        DateTime WinLostDate { get; set; }

        long TransId { get; set; }

        long Serial { get; set; }

        int StatusId { get; set; }

        string Status { get; set; }

        decimal MOdds { get; set; }

        decimal Stake { get; set; }

        decimal WinLost { get; set; }

        decimal Comm { get; set; }

        decimal PlayerComm { get; set; }
    }
}