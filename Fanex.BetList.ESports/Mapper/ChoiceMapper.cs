namespace Fanex.BetList.Core.Mapper
{
    using Constants;

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
                ESportsBetTypes.MapXTotalKillsMoneyline,
                ESportsBetTypes.MapXFirstBlood,
                ESportsBetTypes.MapXFirsttoYKills,
                ESportsBetTypes.MapXTotalTowersMoneyline,
                ESportsBetTypes.MapXFirstTierYTower,
                ESportsBetTypes.MapXTotalRoshansMoneyline,
                ESportsBetTypes.MapX1stRoshan,
                ESportsBetTypes.MapX2ndRoshan,
                ESportsBetTypes.MapX3rdRoshan,
                ESportsBetTypes.MapXTotalBarracksMoneyline,
                ESportsBetTypes.MapXBarracks1stLane,
                ESportsBetTypes.MapXBarracks2ndLane,
                ESportsBetTypes.MapXBarracks3rdLane,
                ESportsBetTypes.MapXTotalTurretsMoneyline,
                ESportsBetTypes.MapXFirstTierYTurret,
                ESportsBetTypes.MapXTotalDragonsMoneyline,
                ESportsBetTypes.MapX1stDragon,
                ESportsBetTypes.MapX2ndDragon,
                ESportsBetTypes.MapX3rdDragon,
                ESportsBetTypes.MapXTotalBaronsMoneyline,
                ESportsBetTypes.MapX1stBaron,
                ESportsBetTypes.MapX2ndBaron,
                ESportsBetTypes.MapX3rdBaron,
                ESportsBetTypes.MapXTotalInhibitorsMoneyline,
                ESportsBetTypes.MapX1stInhibitor,
                ESportsBetTypes.MapX2ndInhibitor,
                ESportsBetTypes.MapX3rdInhibitor,
                ESportsBetTypes.MapXTotalTyrantsMoneyline,
                ESportsBetTypes.MapX1stTyrant,
                ESportsBetTypes.MapX2ndTyrant,
                ESportsBetTypes.MapX3rdTyrant,
                ESportsBetTypes.MapXTotalOverlordsMoneyline,
                ESportsBetTypes.MapX1stOverlord,
                ESportsBetTypes.MapX2ndOverlord,
                ESportsBetTypes.MapX3rdOverlord,
                ESportsBetTypes.MapXFirsttoYRounds,
                ESportsBetTypes.MapXFirstHalf,
                ESportsBetTypes.MapXSecondHalf,
                ESportsBetTypes.MapXMostFirstKill,
                ESportsBetTypes.MapXClutches,
                ESportsBetTypes.MapX16thRound,
                ESportsBetTypes.MapXRoundYMoneyline,
                ESportsBetTypes.MapXRoundYTotalKillsMoneyline,
                ESportsBetTypes.MapXRoundYFirstKill
            };

            foreach (var betTypeId in moneylineBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, ESportsBetTypes.MapXMoneyline);
            }
        }

        private void MapForESports2Handicap()
        {
            var handicapBasedBetTypes = new[]
            {
                ESportsBetTypes.MapXTotalTowersHandicap,
                ESportsBetTypes.MapXTotalRoshansHandicap,
                ESportsBetTypes.MapXTotalBarracksHandicap,
                ESportsBetTypes.MapXTotalTurretsHandicap,
                ESportsBetTypes.MapXTotalDragonsHandicap,
                ESportsBetTypes.MapXTotalBaronsHandicap,
                ESportsBetTypes.MapXTotalInhibitorsHandicap,
                ESportsBetTypes.MapXTotalTyrantsHandicap,
                ESportsBetTypes.MapXTotalOverlordsHandicap,
                ESportsBetTypes.MapXRoundsHandicap,
                ESportsBetTypes.MapXClutchesHandicap,
                ESportsBetTypes.MapXRoundYTotalKillsHandicap
            };

            foreach (var betTypeId in handicapBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, ESportsBetTypes.MapXTotalKillsHandicap);
            }
        }

        private void MapForESports2OverUnder()
        {
            var overUnderBasedBetTypes = new[]
            {
                ESportsBetTypes.MapXTotalTowersOverUnder,
                ESportsBetTypes.MapXTotalRoshansOverUnder,
                ESportsBetTypes.MapXTotalBarracksOverUnder,
                ESportsBetTypes.MapXTotalTurretsOverUnder,
                ESportsBetTypes.MapXTotalDragonsOverUnder,
                ESportsBetTypes.MapXTotalBaronsOverUnder,
                ESportsBetTypes.MapXTotalInhibitorsOverUnder,
                ESportsBetTypes.MapXTotalTyrantsOverUnder,
                ESportsBetTypes.MapXTotalOverlordsOverUnder,
                ESportsBetTypes.MapXDurationOverUnderByMins,
                ESportsBetTypes.MapXRoundsOverUnder,
                ESportsBetTypes.MapXRoundYTotalKillsOverUnder,
                ESportsBetTypes.MapXRoundsOverUnderOvertime
            };

            foreach (var betTypeId in overUnderBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, ESportsBetTypes.MapXTotalKillsOverUnder);
            }
        }

        private void MapForESports2OddEven()
        {
            var oddEvenBasedBetTypes = new[]
            {
                ESportsBetTypes.MapXRoundsOddEven,
                ESportsBetTypes.MapXRoundYTotalKillsOddEven,
                ESportsBetTypes.MapXTotalTowersOddEven,
                ESportsBetTypes.MapXTotalRoshansOddEven,
                ESportsBetTypes.MapXTotalBarracksOddEven,
                ESportsBetTypes.MapXTotalTurretsOddEven,
                ESportsBetTypes.MapXTotalDragonsOddEven,
                ESportsBetTypes.MapXTotalBaronsOddEven,
                ESportsBetTypes.MapXTotalInhibitorsOddEven,
                ESportsBetTypes.MapXTotalTyrantsOddEven,
                ESportsBetTypes.MapXTotalOverlordsOddEven
            };

            foreach (var betTypeId in oddEvenBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, ESportsBetTypes.MapXTotalKillsOddEven);
            }
        }

        private void MapForESports2CorrectScore()
        {
            MapIdToBaseId(ESportsBetTypes.MapXFinalRoundBombPlant, ESportsBetTypes.MapXRoundYBombPlant);
        }
    }
}