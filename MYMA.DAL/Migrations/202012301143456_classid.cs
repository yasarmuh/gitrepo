namespace MYMA.Students.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "CurrentClassId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "CurrentClassId");
        }
    }
}
