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
        }
    }
}