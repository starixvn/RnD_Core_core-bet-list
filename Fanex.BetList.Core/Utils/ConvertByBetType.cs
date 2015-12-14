namespace Fanex.BetList.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class ConvertByBetType.
    /// </summary>
    public static partial class ConvertByBetType
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
        private static List<int> oddsBettypes = new List<int>(new int[]
        {
            1,
            2,
            3,
            7,
            8,
            12,
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
            1324
        });

        /// <summary>
        /// The minus.
        /// </summary>
        private static string minus = "<font color='#B50000'>{0}</font>";

        /// <summary>
        /// Gets or sets the minus.
        /// </summary>
        /// <value>The minus.</value>
        public static string Minus
        {
            get { return minus; }
            set { minus = value; }
        }

        /// <summary>
        /// Shows the odds by bet type.
        /// </summary>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>The input odds type if the input bet type is specified, if not, 0 is returned.</returns>
        public static int ShowOddsByBettype(int betType, int oddsType)
        {
            if (oddsBettypes.Contains(betType))
            {
                return oddsType;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the type of the odds by bet.
        /// </summary>
        /// <param name="odds">The odds value.</param>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>The odds type of input bet type.</returns>
        public static double GetOddsByBetType(double odds, int betType, int oddsType)
        {
            bool isShownOddType = ShowOddsByBettype(betType, oddsType) != 0;
            if (isShownOddType && oddsType == 1)
            {
                return odds + 1;
            }
            else if (isShownOddType && oddsType == 5)
            {
                return odds * 100;
            }
            else
            {
                return odds;
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Reviewed")]

        /// <summary>
        /// HDPs the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Handicap value.</returns>
        public static string Hdp(object value)
        {
            return Cast.AsDecimal(value).ToString("0.##");
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Odds value.</returns>
        public static string Odds(object value)
        {
            decimal d = Cast.AsDecimal(value);

            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT3, d));
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT3, d);
            }
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="odds">The odds value.</param>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>Converted value.</returns>
        public static string Odds(object odds, object betType, object oddsType)
        {
            double d = GetOddsByBetType(Convert.ToDouble(odds), Convert.ToInt32(betType), Convert.ToInt32(oddsType));
            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT3, d));
            }
            else if (d == 0)
            {
                return "-";
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT3, d);
            }
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="odds">The odds value.</param>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>Converted value.</returns>
        public static string VirtualHorseRacingOdds(object odds, object betType, object oddsType)
        {
            double d = GetOddsByBetType(Convert.ToDouble(odds), Convert.ToInt32(betType), Convert.ToInt32(oddsType));
            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT3, d));
            }
            else if (d == 0)
            {
                return "0";
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT3, d);
            }
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <returns>Converted value.</returns>
        public static object OddsType(object betType, object oddsType)
        {
            return ShowOddsByBettype(Convert.ToInt32(betType), Convert.ToInt32(oddsType));
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string MOdds(object value)
        {
            decimal d = Cast.AsDecimal(value);

            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT3, d));
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT3, d);
            }
        }

        /// <summary>
        /// Convert odds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string MOdds4(object value)
        {
            decimal d = Cast.AsDecimal(value);

            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT4, d));
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT4, d);
            }
        }

        /// <summary>
        /// Convert win-loss.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Winloss(object value)
        {
            decimal d = Cast.AsDecimal(value);

            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT20, d));
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT20, d);
            }
        }

        /// <summary>
        /// Convert win-loss.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format string.</param>
        /// <returns>Converted value.</returns>
        public static string Winloss(object value, string format)
        {
            decimal d = Cast.AsDecimal(value);

            if (d < 0)
            {
                return string.Format(minus, string.Format(format, d));
            }
            else
            {
                return string.Format(format, d);
            }
        }

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Comm(object value)
        {
            return string.Format(Formatter.DECIMALFORMAT20, value);
        }

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns>Converted value.</returns>
        public static string Comm(object value, string format)
        {
            return string.Format(format, value);
        }

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Comm_Percentage(object value)
        {
            return string.Format(Formatter.DECIMALFORMAT2, Cast.AsDecimal(value) * 100);
        }

        /// <summary>
        /// Convert commission.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string CommValue(object value)
        {
            decimal d = Cast.AsDecimal(value);

            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT40, d));
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT40, d);
            }
        }

        /// <summary>
        /// Convert stake.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Stake(object value)
        {
            return string.Format(Formatter.DECIMALFORMAT2, value);
        }

        /// <summary>
        /// Convert position taking.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string Pt(object value)
        {
            return (Cast.AsDecimal(value) * 100).ToString("N0");
        }

        /// <summary>
        /// Convert position taking.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted value.</returns>
        public static string PtValue(object value)
        {
            decimal d = Cast.AsDecimal(value);

            if (d < 0)
            {
                return string.Format(minus, string.Format(Formatter.DECIMALFORMAT20, d));
            }
            else
            {
                return string.Format(Formatter.DECIMALFORMAT20, d);
            }
        }
    }
}