namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepriceproduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Product", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Product", "PriceSale", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "PriceSale", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tb_Product", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
