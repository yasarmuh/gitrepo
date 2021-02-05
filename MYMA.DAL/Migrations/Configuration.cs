using MYMA.Students.DAL.BaseImpl;

namespace MYMA.Students.DAL.Migrations
{
    internal sealed class Configuration : DataMigrationConfigurationBase<StudentDbContext>
        
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MYMA.Students.DAL.StudentDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
