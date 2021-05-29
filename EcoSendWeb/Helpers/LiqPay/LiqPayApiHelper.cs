using EcoSendWeb.Models.View.Parcel;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;

namespace EcoSendWeb.Helpers.LiqPay
{
    public class LiqPayApiHelper
    {
        private readonly string privateKey;
        private readonly string publicKey;

        public LiqPayApiHelper(string privateKey, string publicKey)
        {
            this.privateKey = privateKey;
            this.publicKey = publicKey;
        }

        public LiqPayRequestVM GetLiqPayRequestVM(decimal price, int parcelId, int points, string resultUrl)
        {
            LiqPayApiRequest request = new LiqPayApiRequest
            {
                PublicKey = publicKey,
                Amount = price,
                Version = 3,
                Action = LiqpayActions.Pay,
                Currency = "UAH",
                Description = $"Parcel: {parcelId}, Price: {price}",
                OrederId = $"{Guid.NewGuid().ToString().Substring(0, 10)}_{parcelId:D4}_{points}",
                ResultUrl = resultUrl
            };

            string strJson = JsonConvert.SerializeObject(request);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strJson);
            string data = Convert.ToBase64String(bytes);

            return new LiqPayRequestVM
            {
                Data = data,
                Signature = GetSignature(data)
            };
        }

        public bool ValidateResponse(string data, string signature)
        {
            return signature == GetSignature(data);
        }

        private string GetSignature(string data)
        {
            string strSign = privateKey + data + privateKey;
            byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(strSign);

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(bytes1);
                return Convert.ToBase64String(hash);
            }
        }

        public LiqPayApiResponse DecodeApiResponse(string data)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(data);
            string json = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            return JsonConvert.DeserializeObject<LiqPayApiResponse>(json);
        }
    }
}