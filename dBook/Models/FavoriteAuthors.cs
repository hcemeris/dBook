using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    [Table("FavoriteAuthors")]
    public class FavoriteAuthors
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FAVORITE_AUTHORS_ID { get; set; }
        public Authors AUTHOR { get; set; }
        public User USER { get; set; }
    }
}