using Ashow.Domain.Model;
using Microsoft.Data.SqlClient;
using Dapper;
using Ashow.Domain.Entity;
using System.Data;
using Ashow.Data.Repository;

namespace Ashow.Data
{
    public class AshowDAO : IAshowRepository
    {
        private readonly IDbConnection db;

        public AshowDAO(IDbConnection db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<FilmeEntity>> GetFilmesPorNomeAsync(string nome)
        {
            var sql = $"SELECT * FROM Filmes WHERE Nome LIKE @Nome";

            return await db.QueryAsync<FilmeEntity>(sql, new { Nome = $"{nome}%" });
        }
        public async Task<IEnumerable<FilmeEntity>> GetFilmesAsync()
        {
            var sql = @"SELECT F.Filme_Id, 
	                        F.Nome, 
	                        F.Ano_Lancamento, 
	                        F.Sinopse, 
	                        F.Classificacao_Indicativa, 
	                        F.Diretor,
	                        F.Tempo_Duracao ,
                            G.Genero_Id,
	                        G.Genero                        
                        FROM Filmes F 
                        INNER JOIN Genero G 
	                        ON F.Genero_Id = G.Genero_Id";

            var filmes = await db.QueryAsync<FilmeEntity, GeneroEntity, FilmeEntity>(
                sql,
                (filmes, genero) =>
                {
                    filmes.Genero = genero;
                    return filmes;
                },
                splitOn: "Genero_Id"
                );

            return filmes;
        }
        public async Task<IEnumerable<SerieEntity>> GetSeriesAsync()
        {
            var sql = @"
                        SELECT 
                            S.Serie_Id AS SerieId,
                            S.Nome,
                            S.Ano_Lancamento AS AnoLancamento,
                            S.Sinopse,
                            S.Qtde_Episodios AS QtdeEpisodios,
                            S.Classificacao_Indicativa,
                            G.Genero_Id,
                            G.Genero
                        FROM Series S
                        INNER JOIN Genero G
                            ON S.Genero_Id = G.Genero_Id";

            var series = await db.QueryAsync<SerieEntity, GeneroEntity, SerieEntity>(
                sql,
                (serie, genero) =>
                {
                    serie.Genero = genero;
                    return serie;
                },
                splitOn: "Genero_Id" 
            );

            return series;
        }
        public async Task<IEnumerable<GeneroEntity>> GetGenerosAsync()
        {
            var sql = $"SELECT * FROM GENERO";

            return await db.QueryAsync<GeneroEntity>(sql);
        }
        public async Task<IEnumerable<FilmeEntity>> GetFilmesPorGeneroAsync(int generoId)
        {
            var sql = @"SELECT
                            F.Filme_Id, 
                            F.Nome, 
                            F.Ano_Lancamento, 
                            F.Sinopse, 
                            F.Classificacao_Indicativa, 
                            F.Diretor, 
                            F.Tempo_Duracao, 
                            G.Genero_Id, 
                            G.Genero
                        FROM Filmes F 
                        INNER JOIN Genero G 
                            ON F.Genero_Id = G.Genero_Id
                        WHERE F.Genero_Id = @generoId";

            var param = new { generoId };

            var filmes = await db.QueryAsync<FilmeEntity, GeneroEntity, FilmeEntity>(
                sql,
                (filmes, genero) =>
                {
                    filmes.Genero = genero;
                    return filmes;
                },
                param,
                splitOn: "Genero_Id"
                );

            return filmes;
        }
        public async Task InserirFilmeAsync(FilmeEntity filme)
        {
            var sql = @"INSERT INTO Filmes (Nome, Genero_Id, Ano_lancamento, Sinopse, Classificacao_indicativa, Diretor, Tempo_duracao)
                    Values (@Nome, @GeneroId, @AnoLancamento, @Sinopse, @ClassificacaoIndicativa, @Diretor, @TempoDuracao)";

            var param = new
            {
                filme.Nome,
                filme.Genero.GeneroId,
                filme.AnoLancamento,
                filme.Sinopse,
                filme.ClassificacaoIndicativa,
                filme.Diretor,
                filme.TempoDuracao
            };

            await db.ExecuteAsync(sql, param);
        }
        public async Task InserirSerieAsync(SerieEntity serie)
        {
            var sql = @"INSERT INTO Series (Nome, Genero_Id, Ano_lancamento, Sinopse, Classificacao_indicativa, Qtde_Episodios)
                    Values (@Nome, @GeneroId, @AnoLancamento, @Sinopse, @ClassificacaoIndicativa, @QtdeEpisodios)";

            var param = new
            {
                serie.Nome,
                serie.Genero.GeneroId,
                serie.AnoLancamento,
                serie.Sinopse,
                serie.ClassificacaoIndicativa,
                serie.QtdeEpisodios
            };

            await db.ExecuteAsync(sql, param);
        }
        public async Task DeletarFilmeAsync(int filmeId)
        {
            var sql = "DELETE FROM FILMES WHERE Filme_Id = @filmeId";

            await db.ExecuteAsync(sql, new { filmeId });
        }
        public async Task DeletarSerieAsync(int serieId)
        {
            var sql = "DELETE FROM SERIES WHERE Serie_Id = @serieId";

            await db.ExecuteAsync(sql, new { serieId });
        }
        public async Task AtualizarFilmeAsync(int filmeId)
        {
            var sql = "Update Filmes where Filme_id = @filmeId";

            await db.ExecuteAsync(sql, new { filmeId });
        }
        public async Task AtualizarFilmeAsync(FilmeEntity filme)
        {
            var sql = @"UPDATE Filmes
                        SET
                            Nome = @Nome,
                            Genero_Id = @GeneroId,
                            Tempo_duracao = @TempoDuracao,
                            Ano_lancamento = @AnoLancamento,
                            Diretor = @Diretor,
                            Sinopse = @Sinopse,
                            Classificacao_indicativa = @ClassificacaoIndicativa
                        WHERE
                            Filme_Id = @FilmeId;";

            var param = new
            {
                filme.Nome,
                filme.Genero.GeneroId,
                filme.TempoDuracao,
                filme.AnoLancamento,
                filme.Diretor,
                filme.Sinopse,
                filme.ClassificacaoIndicativa,
                filme.FilmeId,
            };

            await db.ExecuteAsync(sql, param);

        }
        public async Task AtualizarSerieAsync(SerieEntity serie)
        {
            var sql = @"UPDATE Series
                        SET
                            Nome = @Nome,
                            Genero_Id = @GeneroId,
                            Qtde_Episodios = @QtdeEpisodios,
                            Ano_lancamento = @AnoLancamento,
                            Sinopse = @Sinopse,
                            Classificacao_indicativa = @ClassificacaoIndicativa
                        WHERE
                            Serie_Id = @SerieId;";

            var param = new
            {
                serie.Nome,
                serie.Genero.GeneroId,
                serie.QtdeEpisodios,
                serie.AnoLancamento,
                serie.Sinopse,
                serie.ClassificacaoIndicativa,
                serie.SerieId,
            };

            await db.ExecuteAsync(sql, param);

        }

    }
}

