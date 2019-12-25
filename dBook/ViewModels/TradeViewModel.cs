using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dBook.Models;
namespace dBook.ViewModels
{
    
    public class TradeViewModel
    {
        
        public User send_offer_user { get; set; }
        public User get_offer_user { get; set; }
        public List<MyBooks> send_offer_books { get; set; }
        public List<MyBooks> get_offer_books { get; set; }
    }
}