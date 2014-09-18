namespace Fanex.BetList.Core.Entities
{
    /// <summary>
    /// SystemParlaySerial data class.
    /// </summary>
    public class SystemParlaySerial : ISystemParlaySerial
    {
        public long Serial { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }
    }
}