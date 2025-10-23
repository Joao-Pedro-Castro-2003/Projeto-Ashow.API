using Ashow.Domain.Entity;
using Ashow.Domain.Model;

namespace Ashow.Data.Repository
{
    public interface IAshowRepository
    {
        Task<IEnumerable<FilmeEntity>> GetFilmesPorNomeAsync(string nome);
        Task<IEnumerable<FilmeEntity>> GetFilmesPorGeneroAsync(int generoId);
        Task<IEnumerable<FilmeEntity>> GetFilmesAsync();
        Task<IEnumerable<SerieEntity>> GetSeriesAsync();
        Task<IEnumerable<GeneroEntity>> GetGenerosAsync();
        Task InserirFilmeAsync(FilmeEntity filme);
        Task InserirSerieAsync(SerieEntity serie);
        Task DeletarFilmeAsync(int filmeId);
        Task DeletarSerieAsync(int serieId);
        Task AtualizarFilmeAsync(FilmeEntity filme);
        Task AtualizarSerieAsync(SerieEntity serie);
    }
}
