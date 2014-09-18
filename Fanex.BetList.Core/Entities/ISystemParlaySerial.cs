namespace Fanex.BetList.Core.Entities
{
    /// <summary>
    /// SystemParlaySerial data interface.
    /// </summary>
    public interface ISystemParlaySerial
    {
        long Serial { get; set; }

        int HomeId { get; set; }

        int AwayId { get; set; }
    }
}