namespace Fanex.BetList.Core.Mapper
{
    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
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