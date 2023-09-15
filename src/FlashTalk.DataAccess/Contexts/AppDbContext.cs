using FlashTalk.DataAccess.Constans;
using System.Data;
using System.Data.SqlClient;

namespace FlashTalk.DataAccess;

public class AppDbContext : IDisposable
{
    private readonly string _connectionString;
    private SqlConnection _connection;

    public AppDbContext()
    {
        _connectionString = DbConstans.CONNECTION;
    }

    private SqlConnection OpenConnection()
    {
        if (_connection == null)
        {
            _connection = new SqlConnection(_connectionString);
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
