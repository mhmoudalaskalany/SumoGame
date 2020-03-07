namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitialModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupTeam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Level", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Team", "LevelId", "dbo.Level");
            DropIndex("dbo.Team", new[] { "LevelId" });
            DropTable("dbo.Team");
            DropTable("dbo.Level");
            DropTable("dbo.GroupTeam");
            DropTable("dbo.Group");
        }
    }
}
