namespace Persistanse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ultima : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tareas", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tareas", "Estado", c => c.Int(nullable: false));
        }
    }
}
