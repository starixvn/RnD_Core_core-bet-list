using System;
namespace Fanex.DataConsole
{
    public class EventsX
    {
        public Int32 EventID { get; set; }

        public String Name { get; set; }

        public String Player { get; set; }

        public String ServerTime { get; set; }

        public Int32 MatchID { get; set; }

        public DateTime CreateDate { get; set; }

        public Matches Match { get; set; }

    }

}