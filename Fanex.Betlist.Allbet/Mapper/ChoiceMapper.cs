namespace Fanex.BetList.Core.Mapper
{
    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (var betType = 2402; betType <= 2405; betType++)
            {
                MapIdToBaseId(betType, 2401);
            }
        }
    }
}