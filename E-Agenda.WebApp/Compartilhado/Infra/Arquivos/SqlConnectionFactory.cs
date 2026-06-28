using Microsoft.Data.SqlClient;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}

public sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    private const string NomeConnectionString = "EAgendaWeb";

    public SqlConnection CreateConnection()
    {
        string? connectionString = configuration.GetConnectionString(NomeConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"A connection string {NomeConnectionString} não foi encontrada."
            );
        }

        return new SqlConnection(connectionString);
    }
}