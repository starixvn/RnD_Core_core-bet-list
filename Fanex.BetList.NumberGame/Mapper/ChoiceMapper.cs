namespace Fanex.BetList.Core.Mapper
{
    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            // Last ball O/U (82)
            MapIdToBaseId(82, 81);

            // Last Ball O/E (84)
            MapIdToBaseId(84, 83);

            MapHappy5();
        }

        private void MapHappy5()
        {
            const int baseBetTypeId = 8101;
            const int endOfBetTypeRange = 8120;

            for (var betTypeId = baseBetTypeId + 1; betTypeId <= endOfBetTypeRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, baseBetTypeId);
            }
        }
    }
}