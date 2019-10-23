using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    [Table("AuthorComments")]
    public class AuthorComments
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AUTHOR_COMMENT_ID { get; set; }
        public string COMMENT { get; set; }
        public int POINT { get; set; }
        public Authors AUTHOR { get; set; }
        public User USER { get; set; }

    }
}