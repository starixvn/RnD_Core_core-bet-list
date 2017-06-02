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

            // 1H HDP
            MapIdToBaseId(2799, 9);
        }
    }
}