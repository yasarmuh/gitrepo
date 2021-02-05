﻿using Grpc.Net.Client;
using MYMA.GrpcDataStore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MYMA.DataStore.Client.Services
{
    public class GRPCDataStore : IDataStore<Student>
    {
        string serverAddress;
        public GRPCDataStore() : this("https://10.0.2.2:5001")
        {

        }
        public GRPCDataStore(string address)
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

        public async Task<bool> AddItemAsync(Student item)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            await client.InsertStudentAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            await client.UpdateStudnetAsync(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            await client.RemoveStudnetAsync(new StudentId { Id = id });
            return await Task.FromResult(true);
        }

        public async Task<Student> GetItemAsync(string id)
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            var result = await client.GetStudentAsync(new StudentId { Id = id });
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Student>> GetItemsAsync()
        {
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new StudentService.StudentServiceClient(channel);

            var result = await client.GetAllStudentsAsync(new Empty());
            return await Task.FromResult(result.Students);
        }
    }
}