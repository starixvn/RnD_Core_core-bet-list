namespace Fanex.BetList.Core.Mapper
{
    using System.Diagnostics.CodeAnalysis;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            Initialize();
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        private void Initialize()
        {
            MapForFastMarkets();
            MapForBasketBall();
            MapForPoolHouseAllInOne();
            MapForESports2();

            ////2ND Handicap (17)
            MapIdToBaseId(17, 1);

            // Map '1H Double Chance' of BetRadar to 'Double Chance'
            MapIdToBaseId(151, 24);

            // Map 'Result/Total Goals' to '1H Result/Total Goals'
            MapIdToBaseId(144, 143);

            ////2ND Over/Under (3rd) (178)
            MapIdToBaseId(178, 18);

            ////2ND Handicap (3rd) (183)
            MapIdToBaseId(183, 1);

            ////Home Team Total Corners Over/Under (197)
            MapIdToBaseId(197, 18);

            ////Away Team Total Corners Over/Under (198)
            MapIdToBaseId(198, 18);

            ////1H Home Corner Over/Under (204)
            MapIdToBaseId(204, 18);

            ////1H Away Corner Over/Under (205)
            MapIdToBaseId(205, 18);

            ////Result And First Team To Score (own) (415)
            MapIdToBaseId(415, 172);

            ////Home Team Which Half First Goal (420)
            MapIdToBaseId(420, 419);

            ////Away Team Which Half First Goal (421)
            MapIdToBaseId(421, 419);

            ////First Team 3 Goals (423)
            MapIdToBaseId(423, 422);

            ////1H Both Teams To Score (own) (427)
            MapIdToBaseId(427, 188);

            ////2H Odd/Even (own) (428)
            MapIdToBaseId(428, 184);

            ////2H 1X2 (own) (430)
            MapIdToBaseId(430, 177);

            ////2H Double Chance (own) (431)
            MapIdToBaseId(431, 186);

            ////2H Draw No Bet (own) (432)
            MapIdToBaseId(432, 185);

            ////2H Both Teams To Score (own) (433)
            MapIdToBaseId(433, 146);

            ////Both Halves Over 1.5 Yes/No (own) (434)
            MapIdToBaseId(434, 189);

            ////Both Halves Under 1.5 Yes/No (own) (435)
            MapIdToBaseId(435, 190);

            ////Home To Score In Both Halves (own) (436)
            MapIdToBaseId(436, 147);

            ////Away To Score In Both Halves (own) (437)
            MapIdToBaseId(437, 148);

            ////Home To Win Both Halves (own) (438)
            MapIdToBaseId(438, 133);

            ////Away To Win Both Halves (own) (439)
            MapIdToBaseId(439, 134);

            ////Home To Win Either Half (own) (440)
            MapIdToBaseId(440, 149);

            ////Away To Win Either Half (own) (441)
            MapIdToBaseId(441, 150);

            ////Highest Scoring Half (own) (442)
            MapIdToBaseId(442, 140);

            ////Highest Scoring Half Home Team (own) (443)
            MapIdToBaseId(443, 141);

            ////Highest Scoring Half Away Team (own) (444)
            MapIdToBaseId(444, 142);

            ////Home 1H to Score/ 2H To Score (446)
            MapIdToBaseId(446, 445);

            ////Away 1H to Score/ 2H To Score (447)
            MapIdToBaseId(447, 445);
        }

        private void MapForBasketBall()
        {
            // FT Which team to score the first basket (604)
            MapIdToBaseId(604, 603);

            // FT Which team to score the last basket (605)
            MapIdToBaseId(605, 603);

            // 2H Race to X Points (607)
            MapIdToBaseId(607, 606);

            // Quarter X Race to YY  Points (613)
            MapIdToBaseId(613, 606);

            // Quarter X Home Team Over Under (615)
            MapIdToBaseId(615, 610);

            // Quarter X Away Team Over Under (616)
            MapIdToBaseId(616, 610);

            // Quarter X Which team to score the last basket (617)
            MapIdToBaseId(617, 606);
        }

        private void MapForFastMarkets()
        {
            // Map 'Next 5 Munites' to 'Next 1 Minute'
            MapIdToBaseId(222, 221);

            // Map 'What will happen first in next 5 minutes' to 'What will happen first in next 1 minute'
            MapIdToBaseId(224, 223);

            // Map 'Next 1 Minute Set Piece' to 'What will happen first in next 1 minute'
            MapIdToBaseId(225, 223);

            // Map 'Which combination will happen first in next 1 min' to 'What will happen first in next 1 minute'
            MapIdToBaseId(226, 223);

            // Map 'Which combination will happen first in next 5 mins' to 'What will happen first in next 1 minute'
            MapIdToBaseId(227, 223);
        }

        private void MapForPoolHouseAllInOne()
        {
            // Map 'Away Team Over / Under' to 'Home Team Over / Under'.
            MapIdToBaseId(402, 401);

            // Map '1H Home Team Over/Under' to 'Home Team Over / Under'.
            MapIdToBaseId(403, 401);

            // Map '1H Away Team Over/Under' to 'Home Team Over / Under'.
            MapIdToBaseId(404, 401);

            // Map 'Exact Away Team Goals' to 'Exact Home Team Goals'.
            MapIdToBaseId(409, 407);

            // Map '1H Correct Score (aos)' to 'Correct Score (aos)'.
            MapIdToBaseId(414, 413);

            // Map 'Away Team Over / Under(dec)' to 'Home Team Over / Under(dec)'.
            MapIdToBaseId(462, 461);

            // Map '1H Home Team Over/Under (dec)' to 'Home Team Over / Under(dec)'.
            MapIdToBaseId(463, 461);

            // Map '1H Away Team Over/Under (dec)' to 'Home Team Over / Under(dec)'.
            MapIdToBaseId(464, 461);

            // Map '1H 3-Way Handicap' to '3-Way Handicap'.
            MapIdToBaseId(453, 28);

            // Map '1H Both Teams To Score / Result' to 'Both Teams To Score / Result'.
            MapIdToBaseId(456, 417);

            // Map '1H Both Teams To Score / Total Goals' to 'Both Teams To Score / Total Goals'.
            MapIdToBaseId(457, 418);

            // Map 'Asian 1X2 HDP' to 'Handicap'.
            MapIdToBaseId(465, 1);

            // Map '1H Asian 1X2 HDP' to 'Handicap'.
            MapIdToBaseId(466, 1);
        }

        private void MapForESports2()
        {
            MapForESports2Moneline();
            MapForESports2Handicap();
            MapForESports2OverUnder();
            MapForESports2OddEven();
            MapForESports2CorrectScore();
        }

        private void MapForESports2Moneline()
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