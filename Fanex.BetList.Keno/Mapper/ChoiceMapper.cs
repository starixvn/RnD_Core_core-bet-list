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
            const int BaseBetTypeId = 1501;
            const int MaxBetTypeIdRange = 1599;

            for (int betTypeId = BaseBetTypeId + 1; betTypeId <= MaxBetTypeIdRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, BaseBetTypeId);
            }
        }
    }
}