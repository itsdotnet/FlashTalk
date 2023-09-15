using FlashTalk.DataAccess.IRepositories;
using FlashTalk.Domain.Entities;
using Npgsql;

namespace FlashTalk.DataAccess.Reposiotries;

public class MessageRepository : IMessageRepository
{
    private AppDbContext _appDbContext;

    public MessageRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> CreateAsync(Message message)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using (var command = new NpgsqlCommand("INSERT INTO \"Messages\" (\"From\", \"To\", \"Text\", \"IsDeleted\", \"CreatedAt\") VALUES (@From, @To, @Text, @IsDeleted, @CreatedAt)", connection))
            {
                command.Parameters.AddWithValue("@From", message.From);
                command.Parameters.AddWithValue("@To", message.To);
                command.Parameters.AddWithValue("@Text", message.Text);
                command.Parameters.AddWithValue("@IsDeleted", false);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
                command.Parameters.AddWithValue("@UpdatedAt", null);

                long rowsAffeted = await command.ExecuteNonQueryAsync();

                return rowsAffeted > 0;
            }
        }
    }

    public async Task<bool> DeleteAsync(long messageId)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using (var command = new NpgsqlCommand($"DELETE FROM \"Messages\" WHERE \"Id\" = @MessageId", connection))
            {
                command.Parameters.AddWithValue("@MessageId", messageId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

    public async Task<IEnumerable<Message>> GetAllMessagesForUserAsync(long userId)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using (var command = new NpgsqlCommand("SELECT * FROM \"Messages\" WHERE \"To\" = @UserId", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    List<Message> messages = new List<Message>();

                    while (await reader.ReadAsync())
                    {
                        Message message = new Message
                        {
                            Id = (long)reader["Id"],
                            From = (long)reader["From"],
                            To = (long)reader["To"],
                            Text = (string)reader["Text"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = (DateTime)reader["UpdatedAt"]
                        };

                        messages.Add(message);
                    }

                    return messages;
                }
            }
        }
    }

    public async Task<Message> GetByIdAsync(long messageId)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using (var command = new NpgsqlCommand("SELECT * FROM \"Messages\" WHERE \"Id\" = @MessageId", connection))
            {
                command.Parameters.AddWithValue("@MessageId", messageId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        Message message = new Message
                        {
                            Id = (long)reader["Id"],
                            From = (long)reader["From"],
                            To = (long)reader["To"],
                            Text = (string)reader["Text"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = reader["UpdatedAt"] is DBNull ? (DateTime?)null : (DateTime)reader["UpdatedAt"]
                        };

                        return message;
                    }
                }
            }
            return null;
        }
    }

    public async Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long user1Id, long user2Id)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using (var command = new NpgsqlCommand("SELECT * FROM \"Messages\" WHERE (\"From\" = @User1Id AND \"To\" = @User2Id) OR (\"From\" = @User2Id AND \"To\" = @User1Id)", connection))
            {
                command.Parameters.AddWithValue("@User1Id", user1Id);
                command.Parameters.AddWithValue("@User2Id", user2Id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    List<Message> messages = new List<Message>();

                    while (await reader.ReadAsync())
                    {
                        Message message = new Message
                        {
                            Id = (long)reader["Id"],
                            From = (long)reader["From"],
                            To = (long)reader["To"],
                            Text = (string)reader["Text"],
                            IsDeleted = (bool)reader["IsDeleted"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            UpdatedAt = (DateTime)reader["UpdatedAt"]
                        };

                        messages.Add(message);
                    }

                    return messages;
                }
            }
        }
    }

    public async Task<bool> UpdateAsync(Message message)
    {
        using (NpgsqlConnection connection = _appDbContext.OpenConnection())
        {
            await using (var command = new NpgsqlCommand("UPDATE \"Messages\" SET \"From\" = @From, \"To\" = @To, \"Text\" = @Text, \"UpdatedAt\" = @UpdatedAt WHERE \"Id\" = @Id", connection))
            {
                command.Parameters.AddWithValue("@From", message.From);
                command.Parameters.AddWithValue("@To", message.To);
                command.Parameters.AddWithValue("@Text", message.Text);
                command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                command.Parameters.AddWithValue("@Id", message.Id);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }
}
