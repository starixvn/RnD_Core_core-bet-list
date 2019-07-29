namespace Fanex.BetList.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using Constants;

    public static class ConvertByBetType
    {
        public const string FontOpenTag = "<font color='#B50000'>";
        public const string FontCloseTag = "</font>";

        private const byte PercentageFactor = 100;

        private static List<int> oddsBettypes = new List<int>(new[]
        {
            BetTypes.Handicap,
            BetTypes.OddEven,
            BetTypes.OverUnder,
            BetTypes.FirstHalfHandicap,
            BetTypes.FirstHalfOverUnder,
            BetTypes.FirstHalfOddEven,
            BetTypes.SecondHalfHandicap,
            BetTypes.SecondHalfOverUnder,
            BetTypes.Moneyline,
            BetTypes.FirstHalfMoneyline,
            81,
            82,
            83,
            84,
            85,
            86,
            87,
            88,
            BetTypes.FullTime1X2Handicap,
            BetTypes.FirstHalf1X2Handicap,
            BetTypes.FullTimeGameHandicap,
            BetTypes.SetXWinner,
            BetTypes.SetXGameHandicap,
            BetTypes.SetXTotalGameOverUnder,
            BetTypes.OddEven3rd,
            BetTypes.SecondHalfOverUnder3rd,
            BetTypes.SecondHalfHandicap3rd,
            BetTypes.SecondHalfOddEven,
            BetTypes.CornersOddEven,
            BetTypes.HomeTeamTotalCornersOverUnder,
            BetTypes.AwayTeamTotalCornersOverUnder,
            BetTypes.FirstHalfCornersOddEven,
            BetTypes.FirstHalfHomeCornerOverUnder,
            BetTypes.FirstHalfAwayCornerOverUnder,
            BetTypes.HomeTeamOverUnder,
            BetTypes.AwayTeamOverUnder,
            BetTypes.FirstHalfHomeTeamOverUnder,
            BetTypes.FirstHalfAwayTeamOverUnder,
            BetTypes.SecondHalfOddEvenOwn,
            BetTypes.Asian1X2Handicap,
            BetTypes.FirstHalfAsian1X2Handicap,
            BetTypes.MatchWinner,
            BetTypes.WhichTeamToScoreTheHighestQuarter,
            BetTypes.WhichTeamToScoreTheFirstBasket,
            BetTypes.WhichTeamToScoreTheLastBasket,
            BetTypes.FirstHalfRaceToXPoints,
            BetTypes.SecondHalfRaceToXPoints,
            BetTypes.QuarterXHandicap,
            BetTypes.QuarterXOverUnder,
            BetTypes.QuarterXOddEven,
            BetTypes.QuarterXMoneyline,
            BetTypes.QuarterXRaceToYPoints,
            BetTypes.QuarterXHomeTeamOverUnder,
            BetTypes.QuarterXAwayTeamOverUnder,
            BetTypes.QuarterXWhichTeamToScoreTheLastBasket,
            BetTypes.Sold,
            BetTypes.Bought,
            1003,
            1004,
            BetTypes.CricketMatchWinner,
            BetTypes.SetHandicap,
            BetTypes.MatchTotalGamesOddEven,
            BetTypes.MatchTotalGamesOverUnder,
            BetTypes.MatchGamesHandicap3rd,
            BetTypes.SetXWinner3rd,
            BetTypes.SetXTotalGames3rd,
            BetTypes.SetXGameHandicap3rd,
            BetTypes.SetXTotalGameOddEven,
            BetTypes.SetXGameYWinner,
            2701,
            2702,
            2703,
            2704,
            2705,
            2706,
            2707,
            2801,
            2802,
            2803,
            2804,
            2805,
            2806,
            2807,
            2808,
            2809,
            2811,
            2812,
            8101,
            8102,
            8104
        });

        public static string Minus { get; set; } = "<font color='#B50000'>{0}</font>";

        public static void RegisterMultipleOddsTypes(int betType)
        {
            if (!oddsBettypes.Contains(betType))
            {
                lock (oddsBettypes)
                {
                    if (!oddsBettypes.Contains(betType))
                    {
                        oddsBettypes.Add(betType);
                    }
                }
            }
        }

        public static int ShowOddsByBettype(int betType, int oddsType)
            => oddsBettypes.Contains(betType) ? oddsType : 0;

        public static double GetOddsByBetType(double odds, int betType, int oddsType)
        {
            var isShownOddType = ShowOddsByBettype(betType, oddsType) != 0;

            if (isShownOddType && oddsType == OddsTypes.Decimal)
            {
                return odds + 1;
            }

            if (isShownOddType && oddsType == OddsTypes.US)
            {
                return odds * PercentageFactor;
            }

            return odds;
        }

        public static string Hdp(object value)
            => Cast.AsDecimal(value).ToString("0.##");

        public static string Odds(object value)
        {
            var odds = Cast.AsDecimal(value);

            return odds < 0 ?
                string.Format(Minus, string.Format(Formatter.DECIMALFORMAT3, odds)) :
                string.Format(Formatter.DECIMALFORMAT3, odds);
        }

        public static string Odds(object oddsValue, object betType, object oddsType)
        {
            var odds = GetOddsByBetType(Convert.ToDouble(oddsValue), Convert.ToInt32(betType), Convert.ToInt32(oddsType));

            if (odds < 0)
            {
                return string.Format(Minus, string.Format(Formatter.DECIMALFORMAT3, odds));
            }

            if (odds.Equals(0))
            {
                return "-";
            }

            return string.Format(Formatter.DECIMALFORMAT3, odds);
        }

        public static string VirtualHorseRacingOdds(object oddsValue, object betType, object oddsType)
        {
            var odds = GetOddsByBetType(Convert.ToDouble(oddsValue), Convert.ToInt32(betType), Convert.ToInt32(oddsType));

            if (odds < 0)
            {
                return string.Format(Minus, string.Format(Formatter.DECIMALFORMAT3, odds));
            }

            if (odds.Equals(0))
            {
                return "0";
            }

            return string.Format(Formatter.DECIMALFORMAT3, odds);
        }

        public static object OddsType(object betType, object oddsTypeValue)
            => ShowOddsByBettype(Convert.ToInt32(betType), Convert.ToInt32(oddsTypeValue));

        public static string MOdds(object value)
            => Odds(value);

        public static string MOdds4(object value)
        {
            var odds = Cast.AsDecimal(value);

            return odds < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT4, odds))
                : string.Format(Formatter.DECIMALFORMAT4, odds);
        }

        public static string Winloss(object value)
        {
            var winLoss = Cast.AsDecimal(value);

            return winLoss < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT20, winLoss))
                : string.Format(Formatter.DECIMALFORMAT20, winLoss);
        }

        public static string Winloss(object value, string format)
        {
            decimal winLoss = Cast.AsDecimal(value);

            return winLoss < 0 ? string.Format(Minus, string.Format(format, winLoss)) : string.Format(format, winLoss);
        }

        public static string Comm(object value)
            => string.Format(Formatter.DECIMALFORMAT20, value);

        public static string Comm(object value, string format)
            => string.Format(format, value);

        public static string Comm_Percentage(object value)
            => string.Format(Formatter.DECIMALFORMAT2, Cast.AsDecimal(value) * PercentageFactor);

        public static string CommValue(object value)
        {
            var comValue = Cast.AsDecimal(value);

            return comValue < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT40, comValue))
                : string.Format(Formatter.DECIMALFORMAT40, comValue);
        }

        public static string Stake(object value)
            => string.Format(Formatter.DECIMALFORMAT2, value);

        public static string Pt(object value)
            => (Cast.AsDecimal(value) * PercentageFactor).ToString("N0");

        public static string PtValue(object value)
        {
            decimal positionTaking = Cast.AsDecimal(value);

            return positionTaking < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT20, positionTaking))
                : string.Format(Formatter.DECIMALFORMAT20, positionTaking);
        }
    }
}