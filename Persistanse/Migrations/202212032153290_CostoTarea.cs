namespace Persistanse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostoTarea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tareas", "Costo", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tareas", "Costo");
        }
    }
}
