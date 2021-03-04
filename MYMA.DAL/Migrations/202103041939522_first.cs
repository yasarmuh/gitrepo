namespace MYMA.Students.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UrduName = c.String(nullable: false),
                        DateofBirth = c.DateTime(nullable: false),
                        AdmisstionDate = c.DateTime(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        CurrentClassId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
