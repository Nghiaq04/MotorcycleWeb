namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateThongKe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongKes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThoiGian = c.DateTime(nullable: false),
                        SoTruyCap = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Statisticals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Statisticals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        AccessNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ThongKes");
        }
    }
}
