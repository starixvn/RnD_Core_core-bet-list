namespace Fanex.BetList.Core.Mapper
{
    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            const int BaseBetTypeId = 1701;
            const int MaxBetTypeIdRange = 1705;

            for (int betTypeId = BaseBetTypeId + 1; betTypeId <= MaxBetTypeIdRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BaseBetTypeId);
            }
        }
    }
}