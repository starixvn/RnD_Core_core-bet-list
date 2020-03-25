namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (var betTypeId = Indo4DBetTypes.FourD + 1; betTypeId <= Indo4DBetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Indo4DBetTypes.FourD);
            }
        }
    }
}
