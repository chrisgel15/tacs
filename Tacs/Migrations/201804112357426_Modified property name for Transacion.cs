namespace Tacs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedpropertynameforTransacion : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transactions", name: "CoinId_Id", newName: "Coin_Id");
            RenameColumn(table: "dbo.Transactions", name: "UserId_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_CoinId_Id", newName: "IX_Coin_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_UserId_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Transactions", name: "IX_User_Id", newName: "IX_UserId_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_Coin_Id", newName: "IX_CoinId_Id");
            RenameColumn(table: "dbo.Transactions", name: "User_Id", newName: "UserId_Id");
            RenameColumn(table: "dbo.Transactions", name: "Coin_Id", newName: "CoinId_Id");
        }
    }
}
