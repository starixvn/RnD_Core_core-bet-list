namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// BetRadar BetType : Choice of bet type Highest Scoring Half.
    /// </summary>
    public class Choice140 : Choice133
    {
        /// <summary>
        /// Gets the bet team.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        /// <returns> The choice name.
        /// </returns>
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
                    return CoreLang.Tie;
                default:
                    return string.Empty;
            }
        }
    }
}