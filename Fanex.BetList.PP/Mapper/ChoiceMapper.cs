namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = PragmaticPlayBetTypes.Dragons888 + 1; betTypeId < PragmaticPlayBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.Dragons888);
            }

            MapIdToBaseId(PragmaticPlayBetTypes.TournamentPrize, PragmaticPlayBetTypes.Dragons888);

            for (int betTypeId = PragmaticPlayBetTypes.CandyVillage; betTypeId <= PragmaticPlayBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.Dragons888);
            }
        }
    }
}