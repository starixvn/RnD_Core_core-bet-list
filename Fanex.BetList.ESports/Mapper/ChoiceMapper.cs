namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.ESports.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            MapForESports2Moneyline();
            MapForESports2Handicap();
            MapForESports2OverUnder();
            MapForESports2OddEven();
            MapForESports2CorrectScore();
        }

        private void MapForESports2Moneyline()
        {
            var moneylineBasedBetTypes = new[]
            {
                BetTypes.MapXTotalKillsMoneyline,
                BetTypes.MapXFirstBlood,
                BetTypes.MapXFirsttoYKills,
                BetTypes.MapXTotalTowersMoneyline,
                BetTypes.MapXFirstTierYTower,
                BetTypes.MapXTotalRoshansMoneyline,
                BetTypes.MapX1stRoshan,
                BetTypes.MapX2ndRoshan,
                BetTypes.MapX3rdRoshan,
                BetTypes.MapXTotalBarracksMoneyline,
                BetTypes.MapXBarracks1stLane,
                BetTypes.MapXBarracks2ndLane,
                BetTypes.MapXBarracks3rdLane,
                BetTypes.MapXTotalTurretsMoneyline,
                BetTypes.MapXFirstTierYTurret,
                BetTypes.MapXTotalDragonsMoneyline,
                BetTypes.MapX1stDragon,
                BetTypes.MapX2ndDragon,
                BetTypes.MapX3rdDragon,
                BetTypes.MapXTotalBaronsMoneyline,
                BetTypes.MapX1stBaron,
                BetTypes.MapX2ndBaron,
                BetTypes.MapX3rdBaron,
                BetTypes.MapXTotalInhibitorsMoneyline,
                BetTypes.MapX1stInhibitor,
                BetTypes.MapX2ndInhibitor,
                BetTypes.MapX3rdInhibitor,
                BetTypes.MapXTotalTyrantsMoneyline,
                BetTypes.MapX1stTyrant,
                BetTypes.MapX2ndTyrant,
                BetTypes.MapX3rdTyrant,
                BetTypes.MapXTotalOverlordsMoneyline,
                BetTypes.MapX1stOverlord,
                BetTypes.MapX2ndOverlord,
                BetTypes.MapX3rdOverlord,
                BetTypes.MapXFirsttoYRounds,
                BetTypes.MapXFirstHalf,
                BetTypes.MapXSecondHalf,
                BetTypes.MapXMostFirstKill,
                BetTypes.MapXClutches,
                BetTypes.MapX16thRound,
                BetTypes.MapXRoundYMoneyline,
                BetTypes.MapXRoundYTotalKillsMoneyline,
                BetTypes.MapXRoundYFirstKill
            };

            foreach (var betTypeId in moneylineBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, BetTypes.MapXMoneyline);
            }
        }

        private void MapForESports2Handicap()
        {
            var handicapBasedBetTypes = new[]
            {
                BetTypes.MapXTotalTowersHandicap,
                BetTypes.MapXTotalRoshansHandicap,
                BetTypes.MapXTotalBarracksHandicap,
                BetTypes.MapXTotalTurretsHandicap,
                BetTypes.MapXTotalDragonsHandicap,
                BetTypes.MapXTotalBaronsHandicap,
                BetTypes.MapXTotalInhibitorsHandicap,
                BetTypes.MapXTotalTyrantsHandicap,
                BetTypes.MapXTotalOverlordsHandicap,
                BetTypes.MapXRoundsHandicap,
                BetTypes.MapXClutchesHandicap,
                BetTypes.MapXRoundYTotalKillsHandicap
            };

            foreach (var betTypeId in handicapBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, BetTypes.MapXTotalKillsHandicap);
            }
        }

        private void MapForESports2OverUnder()
        {
            var overUnderBasedBetTypes = new[]
            {
                BetTypes.MapXTotalTowersOverUnder,
                BetTypes.MapXTotalRoshansOverUnder,
                BetTypes.MapXTotalBarracksOverUnder,
                BetTypes.MapXTotalTurretsOverUnder,
                BetTypes.MapXTotalDragonsOverUnder,
                BetTypes.MapXTotalBaronsOverUnder,
                BetTypes.MapXTotalInhibitorsOverUnder,
                BetTypes.MapXTotalTyrantsOverUnder,
                BetTypes.MapXTotalOverlordsOverUnder,
                BetTypes.MapXDurationOverUnderByMins,
                BetTypes.MapXRoundsOverUnder,
                BetTypes.MapXRoundYTotalKillsOverUnder,
                BetTypes.MapXRoundsOverUnderOvertime
            };

            foreach (var betTypeId in overUnderBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, BetTypes.MapXTotalKillsOverUnder);
            }
        }

        private void MapForESports2OddEven()
        {
            var oddEvenBasedBetTypes = new[]
            {
                BetTypes.MapXRoundsOddEven,
                BetTypes.MapXRoundYTotalKillsOddEven,
                BetTypes.MapXTotalTowersOddEven,
                BetTypes.MapXTotalRoshansOddEven,
                BetTypes.MapXTotalBarracksOddEven,
                BetTypes.MapXTotalTurretsOddEven,
                BetTypes.MapXTotalDragonsOddEven,
                BetTypes.MapXTotalBaronsOddEven,
                BetTypes.MapXTotalInhibitorsOddEven,
                BetTypes.MapXTotalTyrantsOddEven,
                BetTypes.MapXTotalOverlordsOddEven
            };

            foreach (var betTypeId in oddEvenBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, BetTypes.MapXTotalKillsOddEven);
            }
        }

        private void MapForESports2CorrectScore()
        {
            MapIdToBaseId(BetTypes.MapXFinalRoundBombPlant, BetTypes.MapXRoundYBombPlant);
        }
    }
}