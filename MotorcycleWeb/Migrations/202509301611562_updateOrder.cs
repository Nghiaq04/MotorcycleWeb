namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_OrderDetail", "Price", c => c.Long(nullable: false));
            AlterColumn("dbo.tb_Order", "TotalAmount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Order", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tb_OrderDetail", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
