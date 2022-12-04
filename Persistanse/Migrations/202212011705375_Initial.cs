namespace Persistanse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Enabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Enabled", c => c.Boolean());
        }
    }
}
