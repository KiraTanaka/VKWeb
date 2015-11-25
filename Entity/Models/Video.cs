using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Entity.Models
{
    [Table("popular_video")]
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int Vid { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        public string Player { get; set; }
    }
}