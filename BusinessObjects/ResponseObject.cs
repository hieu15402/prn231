using System.Text.Json.Serialization;

namespace FlowerBouquetWebAPI.BusinessObjects
{
    public class ResponseObject<T>
    {
        public string message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? data { get; set; }

        public ResponseObject(string message, T? data = default)
        {
            this.message = message;
            this.data = data;
        }

    }
}
