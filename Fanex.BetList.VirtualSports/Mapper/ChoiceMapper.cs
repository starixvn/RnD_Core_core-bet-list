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
            // Place
            MapIdToBaseId(1232, 1231);

            // Win/Place
            MapIdToBaseId(1233, 1231);

            // 1H O/U
            MapIdToBaseId(2704, 2703);

            // 1H HDP
            MapIdToBaseId(2706, 2705);

            // Basketball Over/Under
            MapIdToBaseId(2803, 2703);

            // Basketball 1H Over/Under
            MapIdToBaseId(2804, 2703);

            // Basketball Handicap
            MapIdToBaseId(2805, 2705);

            // Basketball 1H Handicap
            MapIdToBaseId(2806, 2705);

            // Basketball Home Team Over/Under
            MapIdToBaseId(2811, 2703);

            // Basketball Away Team Over/Under
            MapIdToBaseId(2812, 2703);
        }
    }
}