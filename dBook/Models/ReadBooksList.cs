using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    [Table("ReadBooksList")]
    public class ReadBooksList
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int READBOOKSLIST_ID { get; set; }
        public User USER { get; set; }
        public Books BOOK { get; set; }

    }
}