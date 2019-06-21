namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class ParentChildMapper : BaseMapper
    {
        public ParentChildMapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            MapIdToBaseId(BetTypes.SecondHalfOverUnder3rd, BetTypes.SecondHalfOverUnder);
            MapIdToBaseId(BetTypes.ExactFirstHalfGoals3rd, BetTypes.ExactFirstHalfGoals);
            MapIdToBaseId(BetTypes.SecondHalfHandicap3rd, BetTypes.SecondHalfHandicap);
            MapIdToBaseId(BetTypes.ExactSecondHalfGoals3rd, BetTypes.ExactSecondHalfGoals);
            MapIdToBaseId(BetTypes.ResultAndFirstTeamToScoreOwn, BetTypes.ResultAndFirstTeamToScore);
            MapIdToBaseId(BetTypes.HalfTimeFullTimeCorrectScoreAos, BetTypes.HalfTimeFullTimeCorrectScore);
            MapIdToBaseId(BetTypes.FirstHalfBothTeamsToScoreOwn, BetTypes.FirstHalfBothTeamsToScore);
            MapIdToBaseId(BetTypes.SecondHalfOddEvenOwn, BetTypes.SecondHalfOddEven);
            MapIdToBaseId(BetTypes.SecondHalf1X2Own, BetTypes.SecondHalf1X2);
            MapIdToBaseId(BetTypes.SecondHalfDoubleChanceOwn, BetTypes.SecondHalfDoubleChance);
            MapIdToBaseId(BetTypes.SecondHalfDrawNoBetOwn, BetTypes.SecondHalfDrawNoBet);
            MapIdToBaseId(BetTypes.SecondHalfBothTeamsToScoreOwn, BetTypes.SecondHalfBothTeamsToScore);
            MapIdToBaseId(BetTypes.BothHalvesOver15YesNoOwn, BetTypes.BothHalvesOver15YesNo);
            MapIdToBaseId(BetTypes.BothHalvesUnder15YesNoOwn, BetTypes.BothHalvesUnder15YesNo);
            MapIdToBaseId(BetTypes.HomeToScoreInBothHalvesOwn, BetTypes.HomeToScoreInBothHalves);
            MapIdToBaseId(BetTypes.AwayToScoreInBothHalvesOwn, BetTypes.AwayToScoreInBothHalves);
            MapIdToBaseId(BetTypes.HomeToWinBothHalvesOwn, BetTypes.HomeToWinBothHalves);
            MapIdToBaseId(BetTypes.AwayToWinBothHalvesOwn, BetTypes.AwayToWinBothHalves);
            MapIdToBaseId(BetTypes.HomeToWinEitherHalfOwn, BetTypes.HomeToWinEitherHalf);
            MapIdToBaseId(BetTypes.AwayToWinEitherHalfOwn, BetTypes.AwayToWinEitherHalf);
            MapIdToBaseId(BetTypes.HighestScoringHalfOwn, BetTypes.HighestScoringHalf);
            MapIdToBaseId(BetTypes.HighestScoringHalfHomeTeamOwn, BetTypes.HighestScoringHalfHomeTeam);
            MapIdToBaseId(BetTypes.HighestScoringHalfAwayTeamOwn, BetTypes.HighestScoringHalfAwayTeam);
        }
    }
}