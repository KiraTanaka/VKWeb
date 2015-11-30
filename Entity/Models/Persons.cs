using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Table("access_token")]
    public class AccessTokenDB
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccessToken{ get; set; }
    }
}