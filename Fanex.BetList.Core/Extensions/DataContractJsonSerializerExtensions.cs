using System.IO;
using System.Runtime.Serialization.Json;

namespace Fanex.BetList.Core.Extensions
{
    public static class DataContractJsonSerializerExtensions
    {
        public static T SafeDeserialize<T>(this DataContractJsonSerializer serializer, Stream stream) where T : class, new()
        {
            if (serializer == null || stream == null)
            {
                return new T();
            }

            try
            {
                var result = serializer.ReadObject(stream) as T;
                return result ?? new T();
            }
            catch
            {
                return new T();
            }
        }

        public static T SafeDeserializeFromString<T>(this DataContractJsonSerializer serializer, string json) where T : class, new()
        {
            if (string.IsNullOrEmpty(json) || serializer == null)
            {
                return new T();
            }

            try
            {
                using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
                {
                    var result = serializer.ReadObject(ms) as T;
                    return result ?? new T();
                }
            }
            catch
            {
                return new T();
            }
        }

        public static T SafeDeserialize<T>(this DataContractJsonSerializer serializer, Stream stream, T fallback) where T : class
        {
            if (serializer == null || stream == null)
            {
                return fallback;
            }

            try
            {
                var result = serializer.ReadObject(stream) as T;
                return result ?? fallback;
            }
            catch
            {
                return fallback;
            }
        }

        public static T SafeDeserializeFromString<T>(this DataContractJsonSerializer serializer, string json, T fallback) where T : class
        {
            if (string.IsNullOrEmpty(json) || serializer == null)
            {
                return fallback;
            }

            try
            {
                using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
                {
                    var result = serializer.ReadObject(ms) as T;
                    return result ?? fallback;
                }
            }
            catch
            {
                return fallback;
            }
        }
    }
}
