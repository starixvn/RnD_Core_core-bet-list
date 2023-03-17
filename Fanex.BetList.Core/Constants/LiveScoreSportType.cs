namespace Fanex.BetList.Core.Constants
{
    using System;
    using System.Collections.Generic;
    public static class LiveScoreSportType
    {
       public static readonly Dictionary<int, DateTime> DateTimeSupportLiveScoreBySports = new Dictionary<int, DateTime>()
        {
            { SportsbookSportTypes.BaseBall, new DateTime(2020, 11, 5, 0, 0, 0) },
            { SportsbookSportTypes.IceHockey, new DateTime(2021, 10, 7, 0, 0, 0) },
            { SportsbookSportTypes.Rugby, new DateTime(2021, 10, 7, 0, 0, 0) },
            { SportsbookSportTypes.FieldHockey, new DateTime(2023, 3, 30, 0, 0, 0) }
        };
    }
    
}