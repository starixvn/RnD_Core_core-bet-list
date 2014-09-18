namespace Fanex.BetList.Core.Utils
{
    using System;
    using System.Globalization;
    using System.Web;

    /// <summary>
    /// Formatting utilities.
    /// </summary>
    public class Formatter
    {
        #region FORMAT STRINGS CONST

        public const string CULTNAMEENG = "en-US";
        public const string CULTNAMEVI = "vi-VN";
        public const string DECIMALFORMAT = "{0:#,###}";
        public const string DECIMALFORMAT0 = "{0:#,##0}";
        public const string DECIMALFORMAT1 = "{0:#,##0.#}";
        public const string DECIMALFORMAT10 = "{0:#,##0.0}";
        public const string DECIMALFORMAT2 = "{0:#,##0.##}";
        public const string DECIMALFORMAT20 = "{0:#,##0.00}";

        // Without comma
        public const string DECIMALFORMAT2C = "{0:###0.00}";

        public const string DECIMALFORMAT3 = "{0:#,##0.###}";
        public const string DECIMALFORMAT30 = "{0:#,##0.000}";
        public const string DECIMALFORMAT3C = "{0:###0.000}";
        public const string DECIMALFORMAT4 = "{0:#,##0.####}";
        public const string DECIMALFORMAT40 = "{0:#,##0.0000}";
        public const string DECIMALFORMATVI = "{0:#.###}";
        public const string DECIMALFORMAT0VI = "{0:#.##0}";
        public const string DECIMALFORMAT1VI = "{0:#.##0,#}";
        public const string DECIMALFORMAT10VI = "{0:#.##0,0}";
        public const string DECIMALFORMAT2VI = "{0:#.##0,##}";
        public const string DECIMALFORMAT20VI = "{0:#.##0,00}";
        public const string DECIMALFORMAT2CVI = "{0:#.##0,00}";
        public const string DECIMALFORMAT3VI = "{0:#.##0,###}";
        public const string DECIMALFORMAT30VI = "{0:#.##0,000}";
        public const string DECIMALFORMAT3CVI = "{0:#.##0,000}";
        public const string DECIMALFORMAT4VI = "{0:#.##0,####}";
        public const string DECIMALFORMAT40VI = "{0:#.##0,0000}";

        public const string TIMEFORMATMILISEC = "HH:mm:ss fff";

        #endregion FORMAT STRINGS CONST

        #region CULTURE CONST

        private static CultureInfo englishCult = CultureInfo.CreateSpecificCulture("en-US");
        private static CultureInfo vietnameseCult = CultureInfo.CreateSpecificCulture("vi-VN");
        private static CultureInfo cult = new CultureInfo("en-US");

        #endregion CULTURE CONST

        /// <summary>
        /// Initializes a new instance of the <see cref="Formatter"/> class.
        /// </summary>
        public Formatter()
        {
        }

        public static CultureInfo EngCult
        {
            get
            {
                return englishCult;
            }
        }

        public static CultureInfo VICult
        {
            get
            {
                return vietnameseCult;
            }
        }

        #region Decimal

        /// <summary>
        /// Returns decimal format string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <returns>Converted value string.</returns>
        public static string DecFormat(decimal value, byte decDigits)
        {
            return value.ToString("N" + decDigits);
        }

        /// <summary>
        /// Returns decimal format string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <returns>Converted value string.</returns>
        public static string DecFormat(object value, byte decDigits)
        {
            return DecFormat(value, decDigits, "0");
        }

        /// <summary>
        /// Returns decimal format string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <param name="numberDefault">The number default.</param>
        /// <returns>Converted value string.</returns>
        public static string DecFormat(object value, byte decDigits, string numberDefault)
        {
            return (Convert.IsDBNull(value) || value == null) ? numberDefault : DecFormat(Convert.ToDecimal(value), decDigits);
        }

        /// <summary>
        /// Returns decimal format string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <param name="chop">If set to <c>true</c> [chop].</param>
        /// <returns>Converted value string.</returns>
        public static string DecFormat(decimal value, byte decDigits, bool chop)
        {
            if (!chop)
            {
                return string.Format(DECIMALFORMAT2, value.ToString("N" + decDigits));
            }
            else
            {
                int a = (int)Math.Pow(10, decDigits);

                // Returns the largest integer less than or equal number
                // If the value is negative, then we use Ceiling() function instead of Floor() function
                if (value < 0)
                {
                    return string.Format(DECIMALFORMAT2, Math.Ceiling(value * a) / a);
                }

                return string.Format(DECIMALFORMAT2, Math.Floor(value * a) / a);
            }
        }

        /// <summary>
        /// Returns decimal format string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <param name="chop">If set to <c>true</c> [chop].</param>
        /// <returns>Converted value string.</returns>
        public static string DecFormat(object value, byte decDigits, bool chop)
        {
            return DecFormat(Convert.ToDecimal(value), decDigits, chop);
        }

        #endregion Decimal

        #region Float

        /// <summary>
        /// Floats the format.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <returns>Converted value string.</returns>
        public static string FloatFormat(float value, byte decDigits)
        {
            return value.ToString("N" + decDigits);
        }

        /// <summary>
        /// Floats the format.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <returns>Converted value string.</returns>
        public static string FloatFormat(object value, byte decDigits)
        {
            return FloatFormat(Convert.ToSingle(value), decDigits);
        }

        #endregion Float

        #region Double

        /// <summary>
        /// Doubles the format.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <returns>Converted value string.</returns>
        public static string DoubleFormat(double value, byte decDigits)
        {
            return value.ToString("N" + decDigits);
        }

        /// <summary>
        /// Doubles the format.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decDigits">The dec digits.</param>
        /// <returns>Converted value string.</returns>
        public static string DoubleFormat(object value, byte decDigits)
        {
            return DoubleFormat(Convert.ToDouble(value), decDigits);
        }

        #endregion Double

        #region Number

        /// <summary>
        /// Formats the number1.
        /// </summary>
        /// <param name="objectToFormat">The object to format.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Formatted value string.</returns>
        public static string FormatNumber1(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT1VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT1, objectToFormat);
        }

        /// <summary>
        /// Formats the number2.
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Formatted value string.</returns>
        public static string FormatNumber2(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT2VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT2, objectToFormat);
        }

        /// <summary>
        /// Formats the number20.
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Formatted value string.</returns>
        public static string FormatNumber20(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT20VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT20, objectToFormat);
        }

        /// <summary>
        /// Format number with maximum 3 significant decimal numbers (numbers that # 0).
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber3(object objectToFormat)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            return string.Format(DECIMALFORMAT3, objectToFormat);
        }

        /// <summary>
        /// Format number with 2 decimal, automatically remove redundant '0'
        /// and do not include comma.
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber2C(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT2CVI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT2C, objectToFormat);
        }

        /// <summary>
        /// Formats the number30.
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber30(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT30VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT30, objectToFormat);
        }

        /// <summary>
        /// Format number with 3 decimal, automatically remove redundant '0'
        /// and do not include comma.
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber3C(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT3CVI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT3C, objectToFormat);
        }

        /// <summary>
        /// Format number with 3 decimal, automatically remove redundant '0'.
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber3(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT3VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT3, objectToFormat);
        }

        /// <summary>
        /// Format number with 4 decimal, automatically remove redundant '0'.
        /// </summary>
        /// <param name="objectToFormat">The object.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber4(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT4VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT4, objectToFormat);
        }

        /// <summary>
        /// Formats the number4.
        /// </summary>
        /// <param name="objectToFormat">The objectToFormat to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber4(object objectToFormat)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            return string.Format(DECIMALFORMAT4, objectToFormat);
        }

        /// <summary>
        /// Formats the number40.
        /// </summary>
        /// <param name="objectToFormat">The objectToFormat.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatNumber40(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return "0";
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT40VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT40, objectToFormat);
        }

        /// <summary>
        /// Formats the sign number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatSignNumber(int number)
        {
            if (number > 0)
            {
                return "+" + number.ToString();
            }

            return number.ToString();
        }

        #endregion Number

        #region Date

        /// <summary>
        /// Longs the date format.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string LongDateFormat(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy HH:mm:ss}", date);
        }

        /// <summary>
        /// Shorts the date format.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string ShortDateFormat(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy}", date);
        }

        /// <summary>
        /// Format input date to fully DATETIME format.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FullDateFormat(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy hh:mm:ss tt}", date);
        }

        /// <summary>
        /// Format the input date to Full Date.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FullDateFormat2Ln(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy<br/>hh:mm:ss tt}", date);
        }

        /// <summary>
        /// Format input date to full date.
        /// </summary>
        /// <param name="objectToFormat">The objectToFormat.</param>
        /// <returns>Formatted value string.</returns>
        public static string FullDateFormat2Line(object objectToFormat)
        {
            return FullDateFormat2Ln(objectToFormat);
        }

        /// <summary>
        /// Format date time with only month/day hour:minute.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateTime22(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString("MM/dd hh:mm");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Formats the date time22 DDMM.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateTime22DDMM(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString("dd/MM hh:mm");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Formats the time millisecond.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatTimeMiliSec(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString(TIMEFORMATMILISEC);
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Formats the date en.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateEn(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString("MM/dd/yyyy");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Formats the date vi.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateVi(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Formats the date time en12.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateTimeEn12(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString("MM/dd/yyyy hh:mm:ss tt");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Formats the date time en12 no sec.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateTimeEn12NoSec(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString("MM/dd/yyyy hh:mm tt");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Formats the date time en24.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateTimeEn24(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToDateTime(date).ToString("MM/dd/yyyy HH:mm:ss");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// IF Time is 12:00, then do not return time.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>Converted value string.</returns>
        public static string FormatDateOptionalTime(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            try
            {
                DateTime dt2 = Convert.ToDateTime(date);
                if ((dt2.Hour == 12 || dt2.Hour == 0) && dt2.Minute == 0)
                {
                    return dt2.ToString("MM/dd/yyyy");
                }

                return dt2.ToString("MM/dd/yyyy hh:mm:ss tt");
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Resets the start date.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>The reset date.</returns>
        public static DateTime ResetStartDate(DateTime date)
        {
            int baseMinutes = date.Hour * 60;
            int baseSeconds = (baseMinutes + date.Minute) * 60;

            int totalMilliseconds = ((baseSeconds + date.Second) * 1000) + date.Millisecond;

            return date.AddMilliseconds(-totalMilliseconds);
        }

        /// <summary>
        /// Resets the end date.
        /// </summary>
        /// <param name="date">The date to format.</param>
        /// <returns>The reset date.</returns>
        public static DateTime ResetEndDate(DateTime date)
        {
            return ResetStartDate(date).Add(new TimeSpan(23, 59, 59));
        }

        #endregion Date

        #region String

        /// <summary>
        /// Truncate With Tip.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="hasSpanReturn">If set to <c>true</c> [has span return].</param>
        /// <returns>Converted value string.</returns>
        public static string TruncateWithTip(string value, int maxLength, bool hasSpanReturn)
        {
            if (value.Length > maxLength)
            {
                value = string.Format("<span title='{0}'>{1}...</span>", value.Replace("'", "&#39;"), HttpUtility.HtmlEncode(value.Substring(0, maxLength - 3)));
            }
            else if (hasSpanReturn)
            {
                value = string.Format("<span>{0}</span>", HttpUtility.HtmlEncode(value));
            }

            return value;
        }

        /// <summary>
        /// Truncates the with tip.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns>Formatted value string.</returns>
        public static string TruncateWithTip(string value, int maxLength)
        {
            return TruncateWithTip(value, maxLength, false);
        }

        /// <summary>
        /// Highlight the negative number by red color.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>Converted value string.</returns>
        public static string HighlightNegativeNumber(object amount)
        {
            decimal value = 0;
            if (amount.GetType() == typeof(double))
            {
                value = (decimal)((double)amount);
            }
            else if (amount.GetType() == typeof(decimal))
            {
                value = (decimal)amount;
            }

            return value < 0
                       ? "<font color='#ff0000'>" + DecFormat(amount, 2) + "</font>"
                       : value == 0 ? "0" : DecFormat(amount, 2);
        }

        #endregion String
    }
}