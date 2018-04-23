namespace Tacs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserCoinentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCoins",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        CoinID = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.CoinID })
                .ForeignKey("dbo.Coins", t => t.CoinID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CoinID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCoins", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserCoins", "CoinID", "dbo.Coins");
            DropIndex("dbo.UserCoins", new[] { "CoinID" });
            DropIndex("dbo.UserCoins", new[] { "UserID" });
            DropTable("dbo.UserCoins");
        }
    }
}
