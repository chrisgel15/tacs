namespace Tacs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactionsentityandremovingWalletsentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wallets", "CoinId", "dbo.Coins");
            DropForeignKey("dbo.Wallets", "UserId", "dbo.Users");
            DropIndex("dbo.Wallets", new[] { "CoinId" });
            DropIndex("dbo.Wallets", new[] { "UserId" });
            DropTable("dbo.Wallets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Int(nullable: false),
                        CoinId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Wallets", "UserId");
            CreateIndex("dbo.Wallets", "CoinId");
            AddForeignKey("dbo.Wallets", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Wallets", "CoinId", "dbo.Coins", "Id", cascadeDelete: true);
        }
    }
}
