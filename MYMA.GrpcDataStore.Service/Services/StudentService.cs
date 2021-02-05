using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMA.GrpcDataStore.Service
{
    public class GRPCStudentService : StudentService.StudentServiceBase
    {
        private readonly ILogger<GRPCStudentService> _logger;
        public GRPCStudentService(ILogger<GRPCStudentService> logger)
        {
            _logger = logger;
        }
        public override Task<Student> GetStudent(StudentId request, ServerCallContext context)
        {
            var result = new Student();
            using (MYMA.Students.DAL.StudentDbContext dbContext = new MYMA.Students.DAL.StudentDbContext())
            {
                var record = dbContext.Students.FirstOrDefault(s => s.Id == request.Id);
                result.AdmisstionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(record.AdmisstionDate.ToUniversalTime());
                result.DateofBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(record.DateofBirth.ToUniversalTime());
                result.FirstName = record.FirstName;
                result.LastName = record.LastName;
                result.MiddleName = record.MiddleName;
                result.Id = record.Id;
                result.MobileNumber = record.MobileNumber;

            }
            return Task.FromResult(result);
        }

        public override Task<StudentList> GetAllStudents(Empty request, ServerCallContext context)
        {
            var result = new StudentList();
            using (MYMA.Students.DAL.StudentDbContext dbContext = new MYMA.Students.DAL.StudentDbContext())
            {
                result.Students.AddRange(
                    dbContext.Students.ToList()
                    .Select(y => new Student()
                    {
                        AdmisstionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(y.AdmisstionDate.ToUniversalTime()),
                        DateofBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(y.DateofBirth.ToUniversalTime()),
                        FirstName = y.FirstName,
                        LastName = y.LastName,
                        MiddleName = y.MiddleName,
                        Id = y.Id,
                        MobileNumber = y.MobileNumber
                    }));
            }
            return Task.FromResult(result);
        }

        public override Task<Student> InsertStudent(Student request, ServerCallContext context)
        {
            using (MYMA.Students.DAL.StudentDbContext dbContext = new MYMA.Students.DAL.StudentDbContext())
            {
                var entitytoupdate = dbContext.Students.Add(new Models.Student
                {
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    FirstName = request.FirstName,
                    DateofBirth = request.DateofBirth.ToDateTime(),
                    AdmisstionDate = request.AdmisstionDate.ToDateTime(),
                    MobileNumber = request.MobileNumber,
                    Id=request.Id
                });
                dbContext.SaveChanges();
            }
            return Task.FromResult(request);
        }
        public override Task<TransResult> RemoveStudnet(StudentId request, ServerCallContext context)
        {
            using (MYMA.Students.DAL.StudentDbContext dbContext = new MYMA.Students.DAL.StudentDbContext())
            {
                var todelete = dbContext.Students.FirstOrDefault(s => s.Id == request.Id);
                var entitytoupdate = dbContext.Students.Remove(todelete);
                dbContext.SaveChanges();
            }
            return default;

        }

        public override Task<Student> UpdateStudnet(Student request, ServerCallContext context)
        {
            using (MYMA.Students.DAL.StudentDbContext dbContext = new MYMA.Students.DAL.StudentDbContext())
            {
                var entitytoupdate = dbContext.Students.SingleOrDefault(s => s.Id == request.Id);
                if (entitytoupdate != null)
                {
                    entitytoupdate.LastName = request.LastName;
                    entitytoupdate.MiddleName = request.MiddleName;
                    entitytoupdate.FirstName = request.FirstName;
                    entitytoupdate.DateofBirth = request.DateofBirth.ToDateTime();
                    entitytoupdate.AdmisstionDate = request.AdmisstionDate.ToDateTime();
                    entitytoupdate.MobileNumber = request.MobileNumber;
                    dbContext.SaveChanges();
                }
            }
            return default;
        }
    }
}
