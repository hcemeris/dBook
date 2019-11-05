namespace dBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dBookContext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BOOK_PHOTO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BOOK_PHOTO");
        }
    }
}
