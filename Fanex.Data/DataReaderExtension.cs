using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;

namespace Fanex.Data
{

    public static class DataReaderExtension
    {
        public static T GetValue<T>(this IDataReader reader, string name)
        {
            return (T)Convert.ChangeType(reader[name], typeof(T));
        }

        public static T GetValue<T>(this IDataReader reader, int index)
        {
            return (T)Convert.ChangeType(reader[index], typeof(T));
        }

        public static bool IsDBNull(this IDataReader reader, string name)
        {
            return reader.IsDBNull(reader.GetOrdinal(name));
        }

        public static bool TryGetValue<T>(this IDataReader reader, string name, out T output)
        {
            try
            {
                output = reader.GetValue<T>(name);
                return true;
            }

            catch (Exception ex)
            {
                if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
                {
                    output = default(T);
                    return false;
                }

                else
                    throw;
            }
        }

        /// <summary>
        /// Check DBNull before getting value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T To<T>(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
            {
                return GetValue<T>(reader, name);
            }
            return default(T);
        }

        public static string ToString(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
                return reader[name].ToString();
            else
                return string.Empty;
        }

        public static bool ToBool(this IDataReader reader, string name)
        {
            return ToBool(reader, name, false);
        }

        public static bool ToBool(this IDataReader reader, string name, bool defaultValue)
        {
            if (reader[name] != DBNull.Value)
                return bool.Parse(reader[name].ToString());
            else
                return defaultValue;
        }

        public static Boolean? ToBool2(this IDataReader reader, string name)
        {
            Boolean? result = null;
            if (reader[name] != DBNull.Value)
                result = Convert.ToBoolean(reader[name]);
            return result;

        }

        public static int ToInt(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
            {
                return Convert.ToInt32(reader[name]);
            }
            else
                return 0;
        }

        public static decimal ToDecimal(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
            {
                return Convert.ToDecimal(reader[name]);
            }
            else
                return 0;
        }

        public static Guid ToGuid(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
            {
                return new Guid(reader[name].ToString());
            }
            else
                return Guid.Empty;
        }
        /// <summary>
        /// If reader[name] is null or DBNull,return DateTime.MinValue
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
            {
                return Convert.ToDateTime(reader[name]);
            }
            else
                return DateTime.MinValue;
        }
        /// <summary>
        /// Return null if reader[name] is null or DBNull.Value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime2(this IDataReader reader, string name)
        {
            DateTime? result = null;
            if (reader[name] != DBNull.Value)
                result = Convert.ToDateTime(reader[name]);
            return result;

        }

        public static Char ToChar(this IDataReader reader, string name)
        {
            if (reader[name] != DBNull.Value)
                return Convert.ToChar(reader[name]);
            else
                return ' ';
        }

        public static Byte[] ToByteArray(this IDataReader reader, string name)
        {
            Byte[] blob = null;
            if (reader[name] != DBNull.Value)
            {
                //return Convert.ToInt32(reader[name]);
                //reader.GetBytes(0, 
                //todo wire up the tobyeArray method
                try
                {
                    blob = new Byte[(reader.GetBytes(0, 0, null, 0, int.MaxValue))];
                    reader.GetBytes(0, 0, blob, 0, blob.Length);
                    //reader .GetBytes[0, 0, blob, 0, blob.Length);
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Exception: " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }


            }
            return blob;
        }

    }
}
