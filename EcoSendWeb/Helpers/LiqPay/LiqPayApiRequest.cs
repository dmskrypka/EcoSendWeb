using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EcoSendWeb.Helpers.LiqPay
{
    public class LiqPayApiRequest
    {
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqpayActions Action { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order_id")]
        public string OrederId { get; set; }

        [JsonProperty("result_url")]
        public string ResultUrl { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}