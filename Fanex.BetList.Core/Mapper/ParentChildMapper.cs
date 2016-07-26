namespace Fanex.BetList.Core.Mapper
{
    public class ParentChildMapper : BaseMapper
    {
        public ParentChildMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            ////Result And First Team To Score (own)
            MapIdToBaseId(415, 172);

            ////Half Time/ Full Time Correct Score(own)
            MapIdToBaseId(416, 152);

            ////1H Both Teams To Score (own)
            MapIdToBaseId(427, 188);

            ////2H Odd/Even (own)
            MapIdToBaseId(428, 184);

            ////2H 1X2 (own)
            MapIdToBaseId(430, 177);

            ////2H Double Chance (own)
            MapIdToBaseId(431, 186);

            ////2H Draw No Bet (own)
            MapIdToBaseId(432, 185);

            ////2H Both Teams To Score (own)
            MapIdToBaseId(433, 146);

            ////Both Halves Over 1.5 Yes/No (own)
            MapIdToBaseId(434, 189);

            ////Both Halves Under 1.5 Yes/No (own)
            MapIdToBaseId(435, 190);

            ////Home To Score In Both Halves (own)
            MapIdToBaseId(436, 147);

            ////Away To Score In Both Halves (own)
            MapIdToBaseId(437, 148);

            ////Home To Win Both Halves (own)
            MapIdToBaseId(438, 133);

            ////Away To Win Both Halves (own)
            MapIdToBaseId(439, 134);

            ////Home To Win Either Half (own)
            MapIdToBaseId(440, 149);

            ////Away To Win Either Half (own)
            MapIdToBaseId(441, 150);

            ////Highest Scoring Half (own)
            MapIdToBaseId(442, 140);

            ////Highest Scoring Half Home Team (own)
            MapIdToBaseId(443, 141);

            ////Highest Scoring Half Away Team (own)
            MapIdToBaseId(444, 142);

            ////2ND Over/Under (3rd).
            MapIdToBaseId(178, 18);

            ////2ND Handicap (3rd).
            MapIdToBaseId(183, 17);
        }
    }
}