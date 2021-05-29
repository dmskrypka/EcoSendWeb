using Newtonsoft.Json;
using System;

namespace EcoSendWeb.Helpers.LiqPay
{
    public class LiqPayApiResponse
    {
        [JsonProperty("payment_id")]
        public int PaymentId { get; set; }

        [JsonProperty("action")]
        public LiqpayActions Action { get; set; }

        [JsonProperty("status")]
        public LiqpayStatuses Status { get; set; }

        [JsonProperty("err_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("err_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("paytype")]
        public string PayType { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("acq_id")]
        public int AcqId { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("liqpay_order_id")]
        public string LiqpayOrderId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sender_card_mask2")]
        public string SenderCardMask2 { get; set; }

        [JsonProperty("sender_card_bank")]
        public string SenderCardBank { get; set; }

        [JsonProperty("sender_card_type")]
        public string SenderCardType { get; set; }

        [JsonProperty("sender_card_country")]
        public int SenderCardCountry { get; set; }

        [JsonProperty("ip")]
        public string IP { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("sender_commission")]
        public decimal SenderCommission { get; set; }

        [JsonProperty("receiver_commission")]
        public decimal ReceiverCommission { get; set; }

        [JsonProperty("agent_commission")]
        public decimal AgentCommission { get; set; }

        [JsonProperty("mpi_eci")]
        public int MpiEci { get; set; }

        [JsonProperty("is_3ds")]
        public bool Is3ds { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        //[JsonProperty("create_date")]
        //public DateTime CreateDate { get; set; }

        //[JsonProperty("end_date")]
        //public DateTime EndDate { get; set; }

        [JsonProperty("transaction_id")]
        public int TransactionId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}