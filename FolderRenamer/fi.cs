using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderRenamer
{

    public class FileSystemInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(FileSystemInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var jObject = JObject.Load(reader);
            var fullPath = jObject["FullPath"].Value<string>();
            return Activator.CreateInstance(objectType, fullPath);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var info = value as FileSystemInfo;
            var obj = info == null
                ? null
                : new
                {
                    FullPath = info.FullName
                };
            var token = JToken.FromObject(obj);
            token.WriteTo(writer);
        }
    }
}
