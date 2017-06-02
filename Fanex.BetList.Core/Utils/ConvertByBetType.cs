namespace Fanex.BetList.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class ConvertByBetType.
    /// </summary>
    public static class ConvertByBetType
    {
        /// <summary>
        /// The font open tag.
        /// </summary>
        public const string FontOpenTag = "<font color='#B50000'>";

        /// <summary>
        /// The font close tag.
        /// </summary>
        public const string FontCloseTag = "</font>";

        /// <summary>
        /// The odds bet types.
        /// </summary>
        private static List<int> oddsBettypes = new List<int>(new[]
        {
            1,
            2,
            3,
            7,
            8,
            12,
            17,
            18,
            20,
            21,
            81,
            82,
            83,
            84,
            85,
            86,
            87,
            88,
            124,
            125,
            153,
            154,
            155,
            156,
            157,
            178,
            183,
            184,
            194,
            197,
            198,
            203,
            204,
            205,
            401,
            402,
            403,
            404,
            428,
            501,
            603,
            604,
            605,
            606,
            607,
            609,
            610,
            611,
            612,
            613,
            615,
            616,
            617,
            801,
            803,
            804,
            805,
            806,
            901,
            902,
            1003,
            1301,
            1303,
            1305,
            1306,
            1308,
            1311,
            1312,
            1316,
            1318,
            1324,
            2701,
            2702,
            2703,
            2704,
            2705,
            2706,
            2707
        });

        /// <summary>
        /// Gets or sets the minus.
        /// </summary>
        /// <value>The minus.</value>
        public static string Minus { get; set; } = "<font color='#B50000'>{0}</font>";

        /// <summary>
        /// Shows the odds by bet type.
        /// </summary>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>The input odds type if the input bet type is specified, if not, 0 is returned.</returns>
        public static int ShowOddsByBettype(int betType, int oddsType)
            => oddsBettypes.Contains(betType) ? oddsType : 0;

        /// <summary>
        /// Gets the type of the odds by bet.
        /// </summary>
        /// <param name="odds">The odds value.</param>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>The odds type of input bet type.</returns>
        public static double GetOddsByBetType(double odds, int betType, int oddsType)
        {
            var isShownOddType = ShowOddsByBettype(betType, oddsType) != 0;

            if (isShownOddType && oddsType == 1)
            {
                return odds + 1;
            }

            if (isShownOddType && oddsType == 5)
            {
                return odds * 100;
            }

            return odds;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Reviewed")]
        /// <summary>
        /// HDPs the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Handicap value.</returns>
        public static string Hdp(object value)
            => Cast.AsDecimal(value).ToString("0.##");

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Odds value.</returns>
        public static string Odds(object value)
        {
            var odds = Cast.AsDecimal(value);

            return odds < 0 ?
                string.Format(Minus, string.Format(Formatter.DECIMALFORMAT3, odds)) :
                string.Format(Formatter.DECIMALFORMAT3, odds);
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="oddsValue">The odds value.</param>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>Converted value.</returns>
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

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="oddsValue">The odds value.</param>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>Converted value.</returns>
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

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsTypeValue">Type of the odds.</param>
        /// <returns>Converted value.</returns>
        public static object OddsType(object betType, object oddsTypeValue)
            => ShowOddsByBettype(Convert.ToInt32(betType), Convert.ToInt32(oddsTypeValue));

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string MOdds(object value)
        {
            var odds = Cast.AsDecimal(value);

            return odds < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT3, odds))
                : string.Format(Formatter.DECIMALFORMAT3, odds);
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string MOdds4(object value)
        {
            var odds = Cast.AsDecimal(value);

            return odds < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT4, odds))
                : string.Format(Formatter.DECIMALFORMAT4, odds);
        }

        /// <summary>
        /// Convert win-loss.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Winloss(object value)
        {
            var winLoss = Cast.AsDecimal(value);

            return winLoss < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT20, winLoss))
                : string.Format(Formatter.DECIMALFORMAT20, winLoss);
        }

        /// <summary>
        /// Convert win-loss.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format string.</param>
        /// <returns>Converted value.</returns>
        public static string Winloss(object value, string format)
        {
            decimal winLoss = Cast.AsDecimal(value);

            return winLoss < 0 ? string.Format(Minus, string.Format(format, winLoss)) : string.Format(format, winLoss);
        }

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Comm(object value)
            => string.Format(Formatter.DECIMALFORMAT20, value);

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns>Converted value.</returns>
        public static string Comm(object value, string format)
            => string.Format(format, value);

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Comm_Percentage(object value)
            => string.Format(Formatter.DECIMALFORMAT2, Cast.AsDecimal(value) * 100);

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string CommValue(object value)
        {
            var comValue = Cast.AsDecimal(value);

            return comValue < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT40, comValue))
                : string.Format(Formatter.DECIMALFORMAT40, comValue);
        }

        /// <summary>
        /// Convert stake.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Stake(object value)
            => string.Format(Formatter.DECIMALFORMAT2, value);

        /// <summary>
        /// Convert position taking.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Pt(object value)
            => (Cast.AsDecimal(value) * 100).ToString("N0");

        /// <summary>
        /// Convert position taking.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string PtValue(object value)
        {
            decimal positionTaking = Cast.AsDecimal(value);

            return positionTaking < 0
                ? string.Format(Minus, string.Format(Formatter.DECIMALFORMAT20, positionTaking))
                : string.Format(Formatter.DECIMALFORMAT20, positionTaking);
        }
    }
}