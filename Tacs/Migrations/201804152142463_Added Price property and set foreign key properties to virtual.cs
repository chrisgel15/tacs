namespace Tacs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPricepropertyandsetforeignkeypropertiestovirtual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Price");
        }
    }
}
