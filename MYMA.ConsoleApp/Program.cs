using Grpc.Core;
using Grpc.Net.Client;
using MYMA.GrpcDataStore.Service;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MYMA.ConsoleApp
{
    class Program
    {
         static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var channel = GrpcChannel.ForAddress("https://localhost:5001",
                new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new StudentService.StudentServiceClient(channel);
            await client.InsertStudentAsync(
                new Student
                {
                    AdmisstionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
                    DateofBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
                    FirstName = "FirstName",
                    LastName = "LastName",
                    MiddleName = "MiddleName",
                    Id = Guid.NewGuid().ToString(),
                    MobileNumber = "0300176"
                });


            var result = await client.GetAllStudentsAsync(new Empty());

            foreach (var item in result.Students)
            {
                Console.WriteLine($"{item.FirstName} {item.MiddleName} {item.LastName} {item.DateofBirth.ToDateTime()} {item.AdmisstionDate.ToDateTime()}");
            }
            Console.ReadKey();
        }
    }
}
