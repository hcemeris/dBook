﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    [Table("BookComments")]
    public class BookComments
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BOOK_COMMENT_ID { get; set; }
        public string COMMENT { get; set; }
        public int POINT { get; set; }
        public Books BOOK { get; set; }
        public User USER { get; set; }

    }
}