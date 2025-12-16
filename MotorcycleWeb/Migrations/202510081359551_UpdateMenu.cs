namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "Address");
        }
    }
}
