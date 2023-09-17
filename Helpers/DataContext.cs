namespace LifeCenter.Helpers;

using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

public class DataContext
{
    private DbSettings _dbSettings;

    public DataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
        return new MySqlConnection(connectionString);
    }

    public async Task Init()
    {
        await _initDatabase();
        await _initTables();
    }

    private async Task _initDatabase()
    {
        // create database if it doesn't exist
        var connectionString = $"Server={_dbSettings.Server}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
        using var connection = new MySqlConnection(connectionString);
        var sql = $"CREATE DATABASE IF NOT EXISTS `{_dbSettings.Database}`;";
        await connection.ExecuteAsync(sql);
    }

    private async Task _initTables()
    {
        // create tables if they don't exist
        using var connection = CreateConnection();
        await _initUsers();

        async Task _initUsers()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS Personal (
                    Id INT NOT NULL AUTO_INCREMENT,
                    Name VARCHAR(255),
                    MiddleName VARCHAR(255),
                    LastName VARCHAR(255),
                    Username VARCHAR(255),
                    Password VARCHAR(255),
                    Email VARCHAR(255),
                    Phone INT,
                    Extension INT,
                    Rol INT,
                    FechaCreado TIMESTAMP,
                    CreadoPor INT,
                    FechaModificado TIMESTAMP,
                    ModificadoPor INT,
                    PRIMARY KEY (Id)
                );
            """;
            await connection.ExecuteAsync(sql);
        }
    }
}