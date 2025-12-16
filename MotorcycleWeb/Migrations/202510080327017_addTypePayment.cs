namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTypePayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "TypePayment", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Order", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Order", "TypePayment");
        }
    }
}
