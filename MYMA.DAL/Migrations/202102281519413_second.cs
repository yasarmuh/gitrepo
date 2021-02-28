namespace MYMA.Students.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "UrduName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "UrduName");
        }
    }
}
