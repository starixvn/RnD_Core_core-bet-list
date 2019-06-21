namespace Fanex.BetList.Core.Utils
{
    using System;
    using System.Globalization;
    using System.Web;

    /// <summary>
    /// Formatting utilities.
    /// </summary>
    public static class Formatter
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

        private const string DefaultFormattedNumericValue = "0";
        private const byte DefaultDecimalPrecision = 2;

        #region CULTURE CONST

        private static readonly CultureInfo cult = new CultureInfo("en-US");

        #endregion CULTURE CONST

        public static CultureInfo VICult { get; } = CultureInfo.CreateSpecificCulture("vi-VN");

        #region Decimal

        public static string DecFormat(decimal value)
            => DecFormat(value, DefaultDecimalPrecision);

        public static string DecFormat(decimal value, byte decDigits)
        {
            return value.ToString("N" + decDigits);
        }

        public static string DecFormat(object value)
            => DecFormat(value, DefaultDecimalPrecision);

        public static string DecFormat(object value, byte decDigits)
        {
            return DecFormat(value, decDigits, DefaultFormattedNumericValue);
        }

        public static string DecFormat(object value, byte decDigits, string numberDefault)
        {
            return (Convert.IsDBNull(value) || value == null) ? numberDefault : DecFormat(Convert.ToDecimal(value), decDigits);
        }

        public static string DecFormat(decimal value, byte decDigits, bool chop)
        {
            if (!chop)
            {
                return string.Format(DECIMALFORMAT2, value.ToString("N" + decDigits));
            }

            int a = (int)Math.Pow(10, decDigits);

            // Returns the largest integer less than or equal number
            // If the value is negative, then we use Ceiling() function instead of Floor() function
            if (value < 0)
            {
                return string.Format(DECIMALFORMAT2, Math.Ceiling(value * a) / a);
            }

            return string.Format(DECIMALFORMAT2, Math.Floor(value * a) / a);
        }

        public static string DecFormat(object value, byte decDigits, bool chop)
        {
            return DecFormat(Convert.ToDecimal(value), decDigits, chop);
        }

        #endregion Decimal

        #region Float

        public static string FloatFormat(float value, byte decDigits)
        {
            return value.ToString("N" + decDigits);
        }

        public static string FloatFormat(object value, byte decDigits)
        {
            return FloatFormat(Convert.ToSingle(value), decDigits);
        }

        #endregion Float

        #region Double

        public static string DoubleFormat(double value, byte decDigits)
        {
            return value.ToString("N" + decDigits);
        }

        public static string DoubleFormat(object value, byte decDigits)
        {
            return DoubleFormat(Convert.ToDouble(value), decDigits);
        }

        #endregion Double

        #region Number

        public static string FormatNumber1(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT1VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT1, objectToFormat);
        }

        public static string FormatNumber2(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT2VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT2, objectToFormat);
        }

        public static string FormatNumber20(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT20VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT20, objectToFormat);
        }

        public static string FormatNumber3(object objectToFormat)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            return string.Format(DECIMALFORMAT3, objectToFormat);
        }

        public static string FormatNumber2C(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT2CVI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT2C, objectToFormat);
        }

        public static string FormatNumber30(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT30VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT30, objectToFormat);
        }

        public static string FormatNumber3C(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT3CVI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT3C, objectToFormat);
        }

        public static string FormatNumber3(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT3VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT3, objectToFormat);
        }

        public static string FormatNumber4(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT4VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT4, objectToFormat);
        }

        public static string FormatNumber4(object objectToFormat)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            return string.Format(DECIMALFORMAT4, objectToFormat);
        }

        public static string FormatNumber40(object objectToFormat, string culture)
        {
            if (Convert.IsDBNull(objectToFormat) || objectToFormat == null)
            {
                return DefaultFormattedNumericValue;
            }

            if (culture == CULTNAMEVI)
            {
                return string.Format(DECIMALFORMAT40VI, objectToFormat);
            }

            return string.Format(DECIMALFORMAT40, objectToFormat);
        }

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

        public static string LongDateFormat(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy HH:mm:ss}", date);
        }

        public static string ShortDateFormat(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy}", date);
        }

        public static string FullDateFormat(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy hh:mm:ss tt}", date);
        }

        public static string FullDateFormat2Ln(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return string.Format(cult, "{0:MM/dd/yyyy<br/>hh:mm:ss tt}", date);
        }

        public static string FullDateFormat2Line(object objectToFormat)
        {
            return FullDateFormat2Ln(objectToFormat);
        }

        public static string FormatDateTime22(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("MM/dd hh:mm");
        }

        public static string FormatDateTime22DDMM(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("dd/MM hh:mm");
        }

        public static string FormatTimeMiliSec(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString(TIMEFORMATMILISEC);
        }

        public static string FormatDateEn(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("MM/dd/yyyy");
        }

        public static string FormatDateVi(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
        }

        public static string FormatDateTimeEn12(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        public static string FormatDateTimeEn12NoSec(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("MM/dd/yyyy hh:mm tt");
        }

        public static string FormatDateTimeEn12ShortMonthNoSec(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("M/dd/yyyy hh:mm tt");
        }

        public static string FormatDateTimeEn24(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            return Convert.ToDateTime(date).ToString("MM/dd/yyyy HH:mm:ss");
        }

        public static string FormatDateOptionalTime(object date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return string.Empty;
            }

            DateTime dateTime = Convert.ToDateTime(date);

            if (dateTime == dateTime.Date)
            {
                return dateTime.ToString("MM/dd/yyyy");
            }

            return dateTime.ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        public static DateTime ResetStartDate(DateTime date)
        {
            int baseMinutes = date.Hour * 60;
            int baseSeconds = (baseMinutes + date.Minute) * 60;
            int totalMilliseconds = ((baseSeconds + date.Second) * 1000) + date.Millisecond;

            return date.AddMilliseconds(-totalMilliseconds);
        }

        public static DateTime ResetEndDate(DateTime date)
        {
            var midnightTime = new TimeSpan(23, 59, 59);

            return ResetStartDate(date).Add(midnightTime);
        }

        #endregion Date

        #region String

        public static string TruncateWithTip(string value, int maxLength, bool hasSpanReturn)
        {
            if (value.Length > maxLength)
            {
                const int truncationIndicatorLength = 3;

                return string.Format(
                    "<span title='{0}'>{1}...</span>",
                    value.Replace("'", "&#39;"),
                    HttpUtility.HtmlEncode(value.Substring(0, maxLength - truncationIndicatorLength)));
            }

            if (hasSpanReturn)
            {
                return string.Format("<span>{0}</span>", HttpUtility.HtmlEncode(value));
            }

            return value;
        }

        public static string TruncateWithTip(string value, int maxLength)
        {
            return TruncateWithTip(value, maxLength, false);
        }

        public static string HighlightNegativeNumber(object amount)
        {
            decimal value = 0;

            if ((amount != null) && (amount is double))
            {
                value = (decimal)((double)amount);
            }

            if ((amount != null) && (amount is decimal))
            {
                value = (decimal)amount;
            }

            if (value < 0)
            {
                return $"<font color='#ff0000'>{DecFormat(amount)}</font>";
            }

            if (value > 0)
            {
                return DecFormat(amount);
            }

            return DefaultFormattedNumericValue;
        }

        public static string HighlightNegativeNumber40(object amount)
        {
            var value = Convert.ToDecimal(amount);

            if (value < 0)
            {
                return $"<font color='#ff0000'>{FormatNumber40(amount, CULTNAMEENG)}</font>";
            }

            if (value > 0)
            {
                return FormatNumber40(amount, CULTNAMEENG);
            }

            return DefaultFormattedNumericValue;
        }

        #endregion String
    }
}