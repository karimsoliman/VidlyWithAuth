namespace VidlyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMembershipTypesData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into MemberShipTypes(Name, SignUpFee, DurationInMonthes, Discount) Values('Pay as You Go', 0, 0, 0)");
            Sql("Insert into MemberShipTypes(Name, SignUpFee, DurationInMonthes, Discount) Values('Monthly', 30, 1, 10)");
            Sql("Insert into MemberShipTypes(Name, SignUpFee, DurationInMonthes, Discount) Values('Quarterly', 90, 3, 15)");
            Sql("Insert into MemberShipTypes(Name, SignUpFee, DurationInMonthes, Discount) Values('Annual', 300, 12, 20)");
        }
        
        public override void Down()
        {
        }
    }
}
