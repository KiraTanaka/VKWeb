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
}