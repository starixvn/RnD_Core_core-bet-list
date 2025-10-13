using System.IO;
using System.Runtime.Serialization.Json;

namespace Fanex.BetList.Core.Utils
{
    public static class DataContractJsonSerializerExtensions
    {
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
    }
}
