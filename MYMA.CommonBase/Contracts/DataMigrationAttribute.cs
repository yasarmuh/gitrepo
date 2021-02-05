using System;
using System.Composition;

namespace MYMA.CommonBase.Contracts
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class DataMigrationAttribute : ExportAttribute
    {
        public DataMigrationAttribute() : base(typeof(IDataMigrationService)) { }
        public MigrationBussinessSpace DataMigrationBussinessSpace { get; set; } = MigrationBussinessSpace.Unknown;        
    }
}
