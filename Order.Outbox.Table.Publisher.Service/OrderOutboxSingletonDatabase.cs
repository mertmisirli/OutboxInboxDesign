using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
using Npgsql;
using Order.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//User ID=postgres;Password=mert34;Host=localhost;Port=5432;Database=OutboxInbox
namespace Order.Outbox.Table.Publisher.Service
{
    public static class OrderOutboxSingletonDatabase
    {
        static IDbConnection _connection;
        static bool _dataReaderState = true;
        public static string ConnectionString { get; set; }
        static OrderOutboxSingletonDatabase() 
            => _connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=OrderDB;Username=postgres;Password=mert34");

        public static IDbConnection Connection
        {
            get
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection;
            }
        }
        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql)
             => await _connection.QueryAsync<T>(sql);
        public static async Task<int> ExecuteAsync(string sql)
            => await _connection.ExecuteAsync(sql);
        public static void DataReaderReady()
            => _dataReaderState = true;
        public static void DataReaderBusy()
            => _dataReaderState = false;
        public static bool DataReaderState => _dataReaderState;
    }
}
