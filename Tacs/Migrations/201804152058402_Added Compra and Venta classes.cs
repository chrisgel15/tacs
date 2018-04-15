namespace Tacs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompraandVentaclasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Users", "LastAccessDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastAccessDate");
            DropColumn("dbo.Transactions", "Discriminator");
        }
    }
}
