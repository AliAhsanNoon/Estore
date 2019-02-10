namespace com.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedBaseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isFeatured", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageURL");
            DropColumn("dbo.Products", "isFeatured");
        }
    }
}
