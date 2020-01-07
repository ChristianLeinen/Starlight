using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Starlight.Core
{
    /// <summary>
    /// Provides methods for reading and writing data in the JSON format.
    /// Use https://www.jsonutils.com/ to create classes with
    /// Property Attributes 'DataMember' to de-/serialize your data
    /// </summary>
    public static class Json
    {
        public static T ReadFromStream<T>(Stream stream)
        {
            var settings = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
            var ser = new DataContractJsonSerializer(typeof(T), settings);
            return (T)ser.ReadObject(stream);
        }

        public static T ReadFromFile<T>(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return ReadFromStream<T>(fs);
            }
        }

        public static T ReadFromString<T>(string contents)
        {
            using (var fs = new MemoryStream(Encoding.ASCII.GetBytes(contents)))
            {
                return ReadFromStream<T>(fs);
            }
        }

        public static void WriteToStream<T>(Stream stream, T data)
        {
            var settings = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, false, true, "  "))
            {
                var ser = new DataContractJsonSerializer(typeof(T), settings);
                ser.WriteObject(writer, data);
                writer.Flush();
            }
        }

        public static long WriteToFile<T>(string filename, T data)
        {
            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                WriteToStream(fs, data);
                return fs.Length;
            }
        }

        public static string WriteToString<T>(T data)
        {
            using (var fs = new MemoryStream())
            {
                WriteToStream(fs, data);
                using (StreamReader reader = new StreamReader(fs))
                {
                    fs.Position = 0;
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
