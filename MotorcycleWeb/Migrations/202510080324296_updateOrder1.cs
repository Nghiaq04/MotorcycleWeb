namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "Type");
        }
    }
}
