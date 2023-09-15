using FlashTalk.DataAccess.IRepositories;
using FlashTalk.Domain.Entities;
using Npgsql;

namespace FlashTalk.DataAccess.Reposiotries;

public class UserRepository : IUserRepository
{
    private AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User> CreateAsync(User user)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using var command = new NpgsqlCommand("INSERT INTO \"Users\" (Name, Username, Password, IsDeleted, CreatedAt, UpdatedAt) VALUES (@Name, @Username, @Password, @IsDeleted, @CreatedAt, @UpdatedAt)", connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@IsDeleted", false);
            command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
            command.Parameters.AddWithValue("@UpdatedAt", null);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return await GetByUsernameAsync(user.Username);
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using var command = new NpgsqlCommand($"DELETE FROM \"Users\" WHERE id = {id}", connection);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        { 
            await using var command = new NpgsqlCommand("SELECT * FROM \"Users\"", connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                List<User> users = new List<User>();
                while (reader.Read())
                {
                    User user = new User
                    {
                        Id = (long)reader["Id"],
                        Name = (string)reader["Name"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        IsDeleted = (bool)reader["IsDeleted"],
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        UpdatedAt = (DateTime)reader["UpdatedAt"]
                    };
                    users.Add(user);
                }
                return users;
            }
        }
    }

    public async Task<User> GetByIdAsync(long id)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using var command = new NpgsqlCommand("SELECT * FROM \"Users\" WHERE \"Username\" = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    User user = new User
                    {
                        Id = (long)reader["Id"],
                        Name = (string)reader["Name"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        IsDeleted = (bool)reader["IsDeleted"],
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        UpdatedAt = (DateTime)reader["UpdatedAt"]
                    };
                    return user;
                }
            }
            return null;
        }
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using var command = new NpgsqlCommand("SELECT * FROM \"Users\" WHERE \"Username\" = @Username", connection);
            command.Parameters.AddWithValue("@Username", username);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    User user = new User
                    {
                        Id = (long)reader["Id"],
                        Name = (string)reader["Name"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        IsDeleted = (bool)reader["IsDeleted"],
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        UpdatedAt = (DateTime)reader["UpdatedAt"]
                    };
                    return user;
                }
            }
            return null;
        }
    }

    public async Task<User> UpdateAsync(User user)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using (var command = new NpgsqlCommand("UPDATE \"Users\" SET \"Name\" = @Name, \"Username\" = @Username, \"Password\" = @Password, \"UpdatedAt\" = @UpdatedAt WHERE \"Id\" = @Id", connection))
            {
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                command.Parameters.AddWithValue("@Id", user.Id);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return await GetByUsernameAsync(user.Username);
            }
        }
    }
}
