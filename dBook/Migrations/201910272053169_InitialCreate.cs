namespace dBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorComments",
                c => new
                    {
                        AUTHOR_COMMENT_ID = c.Int(nullable: false, identity: true),
                        COMMENT = c.String(),
                        POINT = c.Int(nullable: false),
                        AUTHOR_AUTHOR_ID = c.Int(),
                        USER_USER_ID = c.Int(),
                    })
                .PrimaryKey(t => t.AUTHOR_COMMENT_ID)
                .ForeignKey("dbo.Authors", t => t.AUTHOR_AUTHOR_ID)
                .ForeignKey("dbo.User", t => t.USER_USER_ID)
                .Index(t => t.AUTHOR_AUTHOR_ID)
                .Index(t => t.USER_USER_ID);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AUTHOR_ID = c.Int(nullable: false, identity: true),
                        AUTHOR_NAME = c.String(),
                        AUTHOR_LASTNAME = c.String(),
                        AUTHOR_DESCRIPTION = c.String(),
                    })
                .PrimaryKey(t => t.AUTHOR_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        USER_ID = c.Int(nullable: false, identity: true),
                        USERNAME = c.String(),
                        NAME = c.String(),
                        LAST_NAME = c.String(),
                        PASSWORD = c.String(),
                        USER_PHOTO = c.String(),
                        REGISTER_DATE = c.DateTime(nullable: false),
                        ROLE = c.String(),
                    })
                .PrimaryKey(t => t.USER_ID);
            
            CreateTable(
                "dbo.BookComments",
                c => new
                    {
                        BOOK_COMMENT_ID = c.Int(nullable: false, identity: true),
                        COMMENT = c.String(),
                        POINT = c.Int(nullable: false),
                        BOOK_BOOK_ID = c.Int(),
                        USER_USER_ID = c.Int(),
                    })
                .PrimaryKey(t => t.BOOK_COMMENT_ID)
                .ForeignKey("dbo.Books", t => t.BOOK_BOOK_ID)
                .ForeignKey("dbo.User", t => t.USER_USER_ID)
                .Index(t => t.BOOK_BOOK_ID)
                .Index(t => t.USER_USER_ID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BOOK_ID = c.Int(nullable: false, identity: true),
                        BOOK_NAME = c.String(),
                        BOOK_DESCRIPTION = c.String(),
                        AUTHOR_AUTHOR_ID = c.Int(),
                        CATEGORY_CATEGORY_ID = c.Int(),
                    })
                .PrimaryKey(t => t.BOOK_ID)
                .ForeignKey("dbo.Authors", t => t.AUTHOR_AUTHOR_ID)
                .ForeignKey("dbo.Category", t => t.CATEGORY_CATEGORY_ID)
                .Index(t => t.AUTHOR_AUTHOR_ID)
                .Index(t => t.CATEGORY_CATEGORY_ID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CATEGORY_ID = c.Int(nullable: false, identity: true),
                        NAME = c.String(),
                    })
                .PrimaryKey(t => t.CATEGORY_ID);
            
            CreateTable(
                "dbo.FavoriteAuthors",
                c => new
                    {
                        FAVORITE_AUTHORS_ID = c.Int(nullable: false, identity: true),
                        AUTHOR_AUTHOR_ID = c.Int(),
                        USER_USER_ID = c.Int(),
                    })
                .PrimaryKey(t => t.FAVORITE_AUTHORS_ID)
                .ForeignKey("dbo.Authors", t => t.AUTHOR_AUTHOR_ID)
                .ForeignKey("dbo.User", t => t.USER_USER_ID)
                .Index(t => t.AUTHOR_AUTHOR_ID)
                .Index(t => t.USER_USER_ID);
            
            CreateTable(
                "dbo.ReadBooksList",
                c => new
                    {
                        READBOOKSLIST_ID = c.Int(nullable: false, identity: true),
                        BOOK_BOOK_ID = c.Int(),
                        USER_USER_ID = c.Int(),
                    })
                .PrimaryKey(t => t.READBOOKSLIST_ID)
                .ForeignKey("dbo.Books", t => t.BOOK_BOOK_ID)
                .ForeignKey("dbo.User", t => t.USER_USER_ID)
                .Index(t => t.BOOK_BOOK_ID)
                .Index(t => t.USER_USER_ID);
            
            CreateTable(
                "dbo.WantReadBooksList",
                c => new
                    {
                        WANTREADBOOKSLIST_ID = c.Int(nullable: false, identity: true),
                        BOOK_BOOK_ID = c.Int(),
                        USER_USER_ID = c.Int(),
                    })
                .PrimaryKey(t => t.WANTREADBOOKSLIST_ID)
                .ForeignKey("dbo.Books", t => t.BOOK_BOOK_ID)
                .ForeignKey("dbo.User", t => t.USER_USER_ID)
                .Index(t => t.BOOK_BOOK_ID)
                .Index(t => t.USER_USER_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WantReadBooksList", "USER_USER_ID", "dbo.User");
            DropForeignKey("dbo.WantReadBooksList", "BOOK_BOOK_ID", "dbo.Books");
            DropForeignKey("dbo.ReadBooksList", "USER_USER_ID", "dbo.User");
            DropForeignKey("dbo.ReadBooksList", "BOOK_BOOK_ID", "dbo.Books");
            DropForeignKey("dbo.FavoriteAuthors", "USER_USER_ID", "dbo.User");
            DropForeignKey("dbo.FavoriteAuthors", "AUTHOR_AUTHOR_ID", "dbo.Authors");
            DropForeignKey("dbo.BookComments", "USER_USER_ID", "dbo.User");
            DropForeignKey("dbo.BookComments", "BOOK_BOOK_ID", "dbo.Books");
            DropForeignKey("dbo.Books", "CATEGORY_CATEGORY_ID", "dbo.Category");
            DropForeignKey("dbo.Books", "AUTHOR_AUTHOR_ID", "dbo.Authors");
            DropForeignKey("dbo.AuthorComments", "USER_USER_ID", "dbo.User");
            DropForeignKey("dbo.AuthorComments", "AUTHOR_AUTHOR_ID", "dbo.Authors");
            DropIndex("dbo.WantReadBooksList", new[] { "USER_USER_ID" });
            DropIndex("dbo.WantReadBooksList", new[] { "BOOK_BOOK_ID" });
            DropIndex("dbo.ReadBooksList", new[] { "USER_USER_ID" });
            DropIndex("dbo.ReadBooksList", new[] { "BOOK_BOOK_ID" });
            DropIndex("dbo.FavoriteAuthors", new[] { "USER_USER_ID" });
            DropIndex("dbo.FavoriteAuthors", new[] { "AUTHOR_AUTHOR_ID" });
            DropIndex("dbo.Books", new[] { "CATEGORY_CATEGORY_ID" });
            DropIndex("dbo.Books", new[] { "AUTHOR_AUTHOR_ID" });
            DropIndex("dbo.BookComments", new[] { "USER_USER_ID" });
            DropIndex("dbo.BookComments", new[] { "BOOK_BOOK_ID" });
            DropIndex("dbo.AuthorComments", new[] { "USER_USER_ID" });
            DropIndex("dbo.AuthorComments", new[] { "AUTHOR_AUTHOR_ID" });
            DropTable("dbo.WantReadBooksList");
            DropTable("dbo.ReadBooksList");
            DropTable("dbo.FavoriteAuthors");
            DropTable("dbo.Category");
            DropTable("dbo.Books");
            DropTable("dbo.BookComments");
            DropTable("dbo.User");
            DropTable("dbo.Authors");
            DropTable("dbo.AuthorComments");
        }
    }
}
