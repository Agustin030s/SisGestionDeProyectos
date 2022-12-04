namespace Persistanse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Finally : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recursoes", "Id_Rol", "dbo.RolEmps");
            DropIndex("dbo.Recursoes", new[] { "Id_Rol" });
            DropColumn("dbo.Recursoes", "Id_Rol");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recursoes", "Id_Rol", c => c.Int(nullable: false));
            CreateIndex("dbo.Recursoes", "Id_Rol");
            AddForeignKey("dbo.Recursoes", "Id_Rol", "dbo.RolEmps", "Id_Rol", cascadeDelete: true);
        }
    }
}
