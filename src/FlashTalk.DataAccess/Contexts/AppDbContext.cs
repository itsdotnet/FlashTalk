using FlashTalk.DataAccess.Constans;
using Npgsql;
using System.Data;

namespace FlashTalk.DataAccess;

public class AppDbContext : IDisposable
{
    private readonly string _connectionString;
    private NpgsqlConnection _connection;

    public AppDbContext()
    {
        _connectionString = DbConstans.CONNECTION;
    }

    public NpgsqlConnection OpenConnection()
    {
        if (_connection == null)
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }
        else if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
        return _connection;
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}
