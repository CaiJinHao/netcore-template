using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Common.Utility.ResponesModels
{
    public class ResponesToKenModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        [JsonIgnore]
        public string Error { get; set; }
        /// <summary>
        /// RSA 签名私匙
        /// </summary>
        public string Pkey { get; set; }
    }
}
