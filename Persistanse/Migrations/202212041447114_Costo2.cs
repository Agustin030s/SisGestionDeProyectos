namespace Persistanse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Costo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proyectos", "Costo", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proyectos", "Costo");
        }
    }
}
