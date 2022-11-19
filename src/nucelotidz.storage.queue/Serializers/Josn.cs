using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace nucelotidz.storage.queue.Serializers
{
    public class Json : ISerializer
    {
        private JsonSerializerOptions jsonSerializerOptions => new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        public string Serialize<T>(T dataObject)
        {
            return JsonSerializer.Serialize(dataObject, jsonSerializerOptions);
        }
        public T Deserialize<T>(string data)
        {            
            return JsonSerializer.Deserialize<T>(data, jsonSerializerOptions);
        }
    }

}
