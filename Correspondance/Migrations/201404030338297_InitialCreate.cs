namespace CCVCorrespondance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Correspondance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Year = c.Int(nullable: false),
                        From = c.String(),
                        Department = c.String(),
                        Subject = c.String(),
                        DateOnLetter = c.DateTime(nullable: false),
                        DateReceivedOrSent = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Correspondance");
        }
    }
}
