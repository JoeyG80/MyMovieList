namespace EFSecurityShell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class models1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnumModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Genre = c.Int(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                        Review_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Reviews", t => t.Review_ID)
                .Index(t => t.Review_ID);
            
            DropColumn("dbo.Reviews", "ReviewGenres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "ReviewGenres", c => c.Int(nullable: false));
            DropForeignKey("dbo.EnumModels", "Review_ID", "dbo.Reviews");
            DropIndex("dbo.EnumModels", new[] { "Review_ID" });
            DropTable("dbo.EnumModels");
        }
    }
}
