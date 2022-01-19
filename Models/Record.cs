using System;
using Newtonsoft.Json;

namespace Recordbook_WebAPI.Models
{
    public class Record
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
        [JsonProperty(PropertyName = "time")]
        public string Time { get; set; }
        [JsonProperty(PropertyName = "speakers")]
        public string[] Speakers { get; set; }
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "tags")]
        public Tag[] Tags { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "parent")]
        public string ParentId { get; set; }
        [JsonProperty(PropertyName = "digitizedOn")]
        public string digitizedOn { get; set; }
        [JsonProperty(PropertyName = "transcribedOn")]
        public string transcribedOn { get; set; }
        [JsonProperty(PropertyName = "transcriptEditedOn")]
        public string transcriptEditedOn { get; set; }
        [JsonProperty(PropertyName = "uploadedOn")]
        public string uploadedOn { get; set; }
    }
}
