namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Statistics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "OriginalPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "OriginalPrice");
        }
    }
}
