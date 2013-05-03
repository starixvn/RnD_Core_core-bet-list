using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.PTEngine.BusinessObjects
{
    public interface IBettingLimitItem
    {
        int Id { get; set; }

        int BetTypeId { get; set; }

        string Name { get; set; }

        string Key { get; set; }

        string ResourceKey { get; set; }

        decimal? MinBet { get; set; }

        decimal? MaxBet { get; set; }

        decimal? MaxPerMatch { get; set; }

        decimal? MaxPerBall { get; set; }

        decimal? MinBetMax { get; set; }

        decimal? MaxBetMin { get; set; }

        decimal? MaxPerMatchMin { get; set; }

        decimal? MaxPerMatchMax { get; set; }

        decimal? MaxPerBallMax { get; set; }

        decimal? MaxPerBallMin { get; set; }
    }
}