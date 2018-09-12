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
            for (int i = 4002; i <= 4099; i++)
            {
                this.MapIdToBaseId(i, 4001);
            }
        }
    }
}