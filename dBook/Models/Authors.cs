using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    [Table("Authors")]
    public class Authors
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AUTHOR_ID { get; set; }
        public string AUTHOR_NAME { get; set; }
        public string AUTHOR_LASTNAME { get; set; }
        public string AUTHOR_DESCRIPTION { get; set; }

    }
}