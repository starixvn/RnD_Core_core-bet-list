namespace Fanex.BetList.Core.Constants
{
    public static class RnGCasinoBetTypes
    {
        public const int Baccarat = 2101;
        public const int BattleWar = 2102;
        public const int BaccaratProgressiveJP = 2178;
        public const int BaccaratProgressiveJPJackpotGame = 2179;
        public const int JackpotContribution = 2198;
        public const int ProgressiveJackpot = 2199;
        public const int MiniBaccarat = 2201;
        public const int MiniProgressiveJackpot = 2299;
        public const int MobileBaccarat = 2301;
        public const int MobileBaccaratProgressiveJP = 2319;
        public const int MobileBaccaratProgressiveJPJackpotGame = 2320;
        public const int MobileJackpotContribution = 2398;
        public const int MobileProgressiveJackpot = 2399;
        public const int TabletBaccarat = 2901;
        public const int TabletBaccaratProgressiveJP = 2919;
        public const int TabletBaccaratProgressiveJPJackpotGame = 2920;
        public const int TabletJackpotContribution = 2998;
        public const int TabletProgressiveJackpot = 2999;

        public static readonly int[] BacarratGameBetTypes = { BaccaratProgressiveJP, MobileBaccaratProgressiveJP, TabletBaccaratProgressiveJP };
        public static readonly int[] JackpotGameBetTypes = { BaccaratProgressiveJPJackpotGame, MobileBaccaratProgressiveJPJackpotGame, TabletBaccaratProgressiveJPJackpotGame };
        public static readonly int[] JackpotContributionBetTypes = { JackpotContribution, MobileJackpotContribution, TabletJackpotContribution };
        public static readonly int[] ProgressiveJackpotBetTypes = { ProgressiveJackpot, MobileProgressiveJackpot, TabletProgressiveJackpot };
    }
}