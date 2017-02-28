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
        }
    }
}