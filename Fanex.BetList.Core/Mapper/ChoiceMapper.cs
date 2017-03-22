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
            ////2ND Handicap (17)
            MapIdToBaseId(17, 1);

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

            MapForFastMarkets();

            MapForBasketBallAIO();
        }

        private void MapForFastMarkets()
        {
            for (var betType = 222; betType <= 225; betType++)
            {
                MapIdToBaseId(betType, 221);
            }
        }

        private void MapForBasketBallAIO()
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
    }
}