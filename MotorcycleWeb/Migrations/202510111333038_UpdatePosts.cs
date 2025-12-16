namespace MotorcycleWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePosts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Post", "Post_Id", "dbo.tb_Post");
            DropIndex("dbo.tb_Post", new[] { "Post_Id" });
            DropColumn("dbo.tb_Post", "Post_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Post", "Post_Id", c => c.Int());
            CreateIndex("dbo.tb_Post", "Post_Id");
            AddForeignKey("dbo.tb_Post", "Post_Id", "dbo.tb_Post", "Id");
        }
    }
}
