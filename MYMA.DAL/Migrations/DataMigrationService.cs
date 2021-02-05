using MYMA.CommonBase.Contracts;
using MYMA.Students.DAL.BaseImpl;

namespace MYMA.Students.DAL.Migrations
{
    [DataMigration(DataMigrationBussinessSpace = MigrationBussinessSpace.Students)]
    internal class DataMigrationService : DataMigrationServiceBase<StudentDbContext, Configuration>
    {

    }
}
