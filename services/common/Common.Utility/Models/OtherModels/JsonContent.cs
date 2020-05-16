using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Common.Utility.Models.OtherModels
{
    public class JsonContent : StringContent
    {
        public JsonContent(string json) :
           base(json, Encoding.UTF8, "application/json")
        { }
    }
}
