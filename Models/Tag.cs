using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recordbook_WebAPI.Models
{
    public class Tag
    {
        [JsonProperty(PropertyName="label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
