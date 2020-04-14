namespace VidlyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingavailabiltyinmovies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "AvailableNumber", c => c.Int(nullable: false));
            Sql("Update Movies SET AvailableNumber = StockNumber");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "AvailableNumber");
        }
    }
}
