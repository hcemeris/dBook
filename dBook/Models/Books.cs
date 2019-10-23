using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    [Table("Books")]
    public class Books
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BOOK_ID { get; set; }
        public string BOOK_NAME { get; set; }
        public string BOOK_DESCRIPTION { get; set; }
        public Authors AUTHOR { get; set; }
        public Category CATEGORY { get; set; }
        

    }
}