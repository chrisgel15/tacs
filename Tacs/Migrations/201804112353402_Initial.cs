namespace Tacs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CoinId_Id = c.Int(nullable: false),
                        UserId_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coins", t => t.CoinId_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId_Id, cascadeDelete: true)
                .Index(t => t.CoinId_Id)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CoinId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coins", t => t.CoinId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CoinId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Wallets", "CoinId", "dbo.Coins");
            DropForeignKey("dbo.Transactions", "UserId_Id", "dbo.Users");
            DropForeignKey("dbo.Transactions", "CoinId_Id", "dbo.Coins");
            DropIndex("dbo.Wallets", new[] { "UserId" });
            DropIndex("dbo.Wallets", new[] { "CoinId" });
            DropIndex("dbo.Transactions", new[] { "UserId_Id" });
            DropIndex("dbo.Transactions", new[] { "CoinId_Id" });
            DropTable("dbo.Wallets");
            DropTable("dbo.Users");
            DropTable("dbo.Transactions");
            DropTable("dbo.Coins");
        }
    }
}
