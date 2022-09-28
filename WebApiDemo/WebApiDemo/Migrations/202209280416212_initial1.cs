namespace WebApiDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Type", c => c.Int(nullable: false));
        }
    }
}
