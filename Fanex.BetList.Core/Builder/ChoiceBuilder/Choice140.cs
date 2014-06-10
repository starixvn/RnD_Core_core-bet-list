namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
        
    /// <summary>
    /// Choice of bet type Highest Scoring Half.
    /// </summary>
    public class Choice140 : Choice133
    {
        protected override string GetBetTeam(string betTeam)
        {
            betTeam = betTeam.ToLower();

            switch (betTeam)
            {
                case "2h":
                    return CoreLang.SecondHalf;
                case "1h":
                    return CoreLang.FirstHalf;
                case "tie":
                default:
                    return CoreLang.Tie;
            }
        }
    }
}