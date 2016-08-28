namespace InterviewPrep.LinqFundamentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        Year = c.Int(nullable: false),
                        Model = c.String(),
                        Liters = c.Single(nullable: false),
                        Cylinders = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                        Highway = c.Int(nullable: false),
                        Combined = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cars");
        }
    }
}
