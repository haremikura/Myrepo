namespace MVCFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditText",
                c => new
                    {
                        FileId = c.Int(nullable: false),
                        Text = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Marker",
                c => new
                    {
                        MarkerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Name = c.String(maxLength: 20, fixedLength: true),
                        Color = c.String(maxLength: 10, fixedLength: true),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MarkerId, t.UserId });
            
            CreateTable(
                "dbo.MarkingLog",
                c => new
                    {
                        MarkerId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Color = c.String(nullable: false, maxLength: 6, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.MarkerId, t.Name, t.Color });
            
            CreateTable(
                "dbo.ServiceUser",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.UserId, t.UserName, t.Password });
            
            CreateTable(
                "dbo.TextFilesList",
                c => new
                    {
                        FileId = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                        Update = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.FileId, t.FileName, t.UserId, t.Update });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TextFilesList");
            DropTable("dbo.ServiceUser");
            DropTable("dbo.MarkingLog");
            DropTable("dbo.Marker");
            DropTable("dbo.EditText");
        }
    }
}
