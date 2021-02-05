using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace MYMA.CommonBase.Contracts
{
    public interface IDataMigrationService
    {
        void Migrate(DbConnection connection);
    }
}
