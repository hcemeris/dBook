using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.Models
{
    [Table("MyBooks")]
    public class MyBooks
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MyBook_ID { get; set; }
        public Books Book { get; set; }
        public User User { get; set; }
    }
}