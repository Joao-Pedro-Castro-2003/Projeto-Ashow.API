using Ashow.Domain.Entity;
using Ashow.Domain.Model;

namespace Ashow.Business.Interfaces
{
    public interface IAshowService
    {
        Task InserirFilmeAsync(FilmeModel filme);
        Task InserirSerieAsync(SerieModel serie);
        Task DeletarFilmeAsync(int filmeId);
        Task DeletarSerieAsync(int serieId);
        Task AtualizarFilmeAsync(FilmeModel filme);
        Task AtualizarSerieAsync(SerieModel serie);
        Task<IEnumerable<FilmeModel>> GetFilmesPorNomeAsync(string nome);
        Task<IEnumerable<FilmeModel>> GetFilmesPorGeneroAsync(int generoId);
        Task<IEnumerable<FilmeModel>> GetFilmesAsync();
        Task<IEnumerable<SerieModel>> GetSeriesAsync();
        Task<IEnumerable<GeneroModel>> GetGenerosAsync();
    }
}
