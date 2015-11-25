using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Models
{

    public class Persons
    {
        [JsonProperty("response")]
        public List<Person> People { get; set; }
    }
    public class ResponseScreenName
    {
        public ResolveScreenName response { get; set; }
    }
    public class ResolveScreenName
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("object_id")]
        public int ObjectId { get; set; }
    }
}