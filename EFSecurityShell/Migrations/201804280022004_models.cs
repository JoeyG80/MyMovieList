namespace EFSecurityShell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieName = c.String(nullable: false),
                        Director = c.String(nullable: false),
                        DateReleased = c.DateTime(nullable: false),
                        Genre = c.Int(nullable: false),
                        Summary = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieID = c.Int(nullable: false),
                        ReviewName = c.String(nullable: false),
                        Score = c.Int(nullable: false),
                        ReviewContent = c.String(nullable: false),
                        ReviewGenres = c.Int(nullable: false),
                        FavoriteGenre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.MovieID);
            
            CreateTable(
                "dbo.Seens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieID = c.Int(nullable: false),
                        DateSeen = c.DateTime(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seens", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "MovieID", "dbo.Movies");
            DropIndex("dbo.Seens", new[] { "MovieID" });
            DropIndex("dbo.Reviews", new[] { "MovieID" });
            DropTable("dbo.Seens");
            DropTable("dbo.Reviews");
            DropTable("dbo.Movies");
        }
    }
}
