using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Entity.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [Required]
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [Required]
        [JsonProperty("uid")]
        public int UID { get; set; }
    }
}