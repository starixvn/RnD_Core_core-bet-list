namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = PragmaticPlayBetTypes.Dragons888 + 1; betTypeId < PragmaticPlayBetTypes.JackpotContribution; betTypeId++)
            {
                MapIdToBaseId(betTypeId, PragmaticPlayBetTypes.Dragons888);
            }

            MapIdToBaseId(PragmaticPlayBetTypes.JackpotPrize, PragmaticPlayBetTypes.TournamentPrize);
        }
    }
}