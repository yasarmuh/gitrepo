using Grpc.Net.Client;
using MYMA.GrpcDataStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MYMA.DataStore.Client.Services
{
    public class GRPCStudentDataStore : IDataStore<Models.Student>
    {
        string serverAddress;
        public GRPCStudentDataStore() : this("https://localhost:5001")
        {

        }
        public GRPCStudentDataStore(string address)
        {
            // var httpHandler = new HttpClientHandler
            // {
            //     ServerCertificateCustomValidationCallback =
            //HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            // };
            // var channel = GrpcChannel.ForAddress(address,
            //     new GrpcChannelOptions { HttpHandler = httpHandler });
            // client = new StudentService.StudentServiceClient(channel);


            serverAddress = address;
        }

        public async Task<bool> AddItemAsync(Models.Student item)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);
            Student toinsert = new Student()
            {
                AdmisstionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.AdmisstionDate),
                DateofBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.DateofBirth),
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MiddleName = item.MiddleName,
                MobileNumber = item.MobileNumber,
                UrduName = item.UrduName
            };
            await client.InsertStudentAsync(toinsert);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Student item)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);
            Student toupdate = new Student()
            {
                AdmisstionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.AdmisstionDate),
                DateofBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.DateofBirth),
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MiddleName = item.MiddleName,
                MobileNumber = item.MobileNumber,
                UrduName = item.UrduName
            };

            await client.UpdateStudnetAsync(toupdate);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            await client.RemoveStudnetAsync(new StudentId { Id = id });
            return await Task.FromResult(true);
        }

        public async Task<Models.Student> GetItemAsync(string id)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            var item = await client.GetStudentAsync(new StudentId { Id = id });
            Models.Student foundstudent = new Models.Student()
            {
                AdmisstionDate = item.AdmisstionDate.ToDateTime(),
                DateofBirth = item.DateofBirth.ToDateTime(),
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MiddleName = item.MiddleName,
                MobileNumber = item.MobileNumber,
                UrduName = item.UrduName
            };
            return await Task.FromResult(foundstudent);
        }

        public async Task<IEnumerable<MYMA.Models.Student>> GetItemsAsync()
        {
            List<MYMA.Models.Student> studentslist = new List<MYMA.Models.Student>();

            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            var result = await client.GetAllStudentsAsync(new Empty());
            foreach (var item in result.Students)
            {
                studentslist.Add(new Models.Student
                {
                    AdmisstionDate = item.AdmisstionDate.ToDateTime(),
                    DateofBirth = item.DateofBirth.ToDateTime(),
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName,
                    MobileNumber = item.MobileNumber,
                    UrduName = item.UrduName
                });
            }
            return studentslist;
        }

    }
}