using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    [Table("User")]
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string USER_PHOTO { get; set; }
        public DateTime REGISTER_DATE { get; set; }
        public string ROLE { get; set; }
    }
}