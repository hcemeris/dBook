using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace dBook.Models
{
    public class dBookContext:DbContext
    {
        public dBookContext() : base("dBookcontext")
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthorComments> AuthorComments { get; set; }
        public DbSet<BookComments> BookComments { get; set; }
        public DbSet<ReadBooksList> ReadBooksLists { get; set; }
        public DbSet<WantReadBooksList> WantReadBooksLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FavoriteAuthors> FavoriteAuthors { get; set; }


    }
}