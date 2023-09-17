namespace LifeCenter.Repositories;

using Dapper;
using LifeCenter.Entities;
using LifeCenter.Helpers;

public interface IPersonalRepository
{
    Task<IEnumerable<Personal>> GetAll();
    Task<Personal> GetById(int id);
    Task<Personal> GetByName(string email);
    Task Create(Personal user);
    Task Update(Personal user);
    Task Delete(int id);
}

public class PersonalRepository : IPersonalRepository
{
    private DataContext _context;

    public PersonalRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Personal>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Personal
        """;
        return await connection.QueryAsync<Personal>(sql);
    }

    public async Task<Personal> GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Personal
            WHERE Id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<Personal>(sql, new { id });
    }

    public async Task<Personal> GetByName(string name)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Personal 
            WHERE Name = @name
        """;
        return await connection.QuerySingleOrDefaultAsync<Personal>(sql, new { name });
    }

    public async Task Create(Personal user)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO Personal ( Name, MiddleName, LastName, Username, Password, Email, Phone, Extension, Rol, FechaCreado, CreadoPor, FechaModificado, ModificadoPor)
            VALUES (@Name, @MiddleName, @LastName, @Username, @Password, @Email, @Phone, @Extension, @Rol, @FechaCreado, @CreadoPor, @FechaModificado, ModificadoPor)
        """;
        await connection.ExecuteAsync(sql, user);
    }

    public async Task Update(Personal user)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            UPDATE Personal
            SET Name = @Name,
                MiddleName = @MiddleName,
                LastName = @LastName,
                Username = @Username,
                Password = @Password,
                Email = @Email,
                Phone = @Phone,
                Extension = @Extension,
                Rol = @Rol,
                FechaCreado = @FechaCreado,
                CreadoPor = @CreadoPor,
                FechaModificado = @FechaModificado,
                ModificadoPor = @ModificadoPor
            WHERE Id = @Id
        """; 
        await connection.ExecuteAsync(sql, user);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            DELETE FROM Personal 
            WHERE Id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}