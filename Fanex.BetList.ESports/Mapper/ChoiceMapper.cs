namespace Fanex.BetList.Core.Mapper
{
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
                9004,
                9006,
                9007,
                9010,
                9011,
                9014,
                9015,
                9016,
                9017,
                9020,
                9021,
                9022,
                9023,
                9026,
                9027,
                9030,
                9031,
                9032,
                9033,
                9036,
                9037,
                9038,
                9039,
                9042,
                9043,
                9044,
                9045,
                9048,
                9049,
                9050,
                9051,
                9054,
                9055,
                9056,
                9057,
                9062,
                9063,
                9064,
                9065,
                9066,
                9067,
                9068,
                9069,
                9072
            };

            foreach (var betTypeId in moneylineBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, 9001);
            }
        }

        private void MapForESports2Handicap()
        {
            var handicapBasedBetTypes = new[]
            {
                9008,
                9012,
                9018,
                9024,
                9028,
                9034,
                9040,
                9046,
                9052,
                9059,
                9076,
                9077
            };

            foreach (var betTypeId in handicapBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, 9002);
            }
        }

        private void MapForESports2OverUnder()
        {
            var overUnderBasedBetTypes = new[]
            {
                9009,
                9013,
                9019,
                9025,
                9029,
                9035,
                9041,
                9047,
                9053,
                9058,
                9060,
                9070,
                9074
            };

            foreach (var betTypeId in overUnderBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, 9003);
            }
        }

        private void MapForESports2OddEven()
        {
            var oddEvenBasedBetTypes = new[]
            {
                9061,
                9071,
                9078,
                9079,
                9080,
                9081,
                9082,
                9083,
                9084,
                9085,
                9086
            };

            foreach (var betTypeId in oddEvenBasedBetTypes)
            {
                MapIdToBaseId(betTypeId, 9005);
            }
        }

        private void MapForESports2CorrectScore()
        {
            // Map 'Map X Final Round Bomb Plant' to 'Map X Round Y Bomb Plant'
            MapIdToBaseId(9075, 9073);
        }
    }
}