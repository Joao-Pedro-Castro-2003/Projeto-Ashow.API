using Ashow.Data.Repository;
using Ashow.Domain.Entity;
using Ashow.Domain.Model;
using Dapper;
using System.Data;

namespace Ashow.Data
{
    public class UsuarioDAO : IUsuarioRepository
    {
        private readonly IDbConnection db;

        public UsuarioDAO(IDbConnection db)
        {
            this.db = db;
        }
        public async Task Cadastrar(UsuarioEntity usuario)
        {
            var sql = @"INSERT INTO Usuarios (Nome, Email, Senha, DataCadastro)
                        VALUES (@Nome, @Email, @Senha, GETDATE());";

            var param = new
            {
                usuario.Nome,
                usuario.Email,
                usuario.Senha
            };

            await db.ExecuteAsync(sql, param);
        }
        public async Task Deletar(int Id)
        {
            var sql = @"DELETE FROM Usuarios WHERE Id = @Id;";

            await db.ExecuteAsync(sql, new { Id });
        }
        public async Task Atualizar(UsuarioEntity usuario)
        {
            var sql = @"UPDATE USUARIOS SET 
                            Nome = @Nome, 
                            Senha = @Senha,
                            Email = @Email,
                            DataCadastro = GETDATE()
                        WHERE ID = @Id";

            var param = new
            {
                usuario.Nome,
                usuario.Senha,
                usuario.Email,
                usuario.Id
            };

            await db.ExecuteAsync(sql, param);
        }
        public async Task<UsuarioEntity> BuscarPorId(int Id)
        {
            var sql = @"SELECT * FROM USUARIOS WHERE ID = @Id";

            return await db.QueryFirstOrDefaultAsync<UsuarioEntity>(sql, new { Id });
        }
        public async Task<UsuarioEntity> BuscaUsuarioPor(string nome)
        {
            var sql = @"SELECT * FROM Usuarios WHERE Nome = @Nome";

            var param = new
            {
                Nome = nome,
            };

            return await db.QueryFirstOrDefaultAsync<UsuarioEntity>(sql, param);
        }
    }
}
