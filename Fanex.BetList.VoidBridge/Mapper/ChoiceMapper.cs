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
            for (int i = 4002; i <= 4099; i++)
            {
                MapIdToBaseId(i, 4001);
            }
        }
    }
}