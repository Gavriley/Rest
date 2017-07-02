using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Models
{
    public class Post
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Name must be filled")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Description must be filled")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
