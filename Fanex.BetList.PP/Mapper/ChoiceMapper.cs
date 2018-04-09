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
            for (int betTypeId = 4102; betTypeId <= 4300; betTypeId++)
            {
                MapIdToBaseId(betTypeId, 4101);
            }
        }
    }
}