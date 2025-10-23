using Ashow.Business.Interfaces;
using Ashow.Data;
using Ashow.Data.Repository;
using Ashow.Domain.Entity;
using Ashow.Domain.Model;
using AutoMapper;

namespace Ashow.Business
{
    public class AshowBL : IAshowService
    {
        private readonly IAshowRepository _ashowRepository;
        private readonly IMapper mapper;

        public AshowBL(IAshowRepository ashowRepository, IMapper map)
        {
            _ashowRepository = ashowRepository;
            mapper = map;
        }

        public async Task<IEnumerable<FilmeModel>> GetFilmesPorNomeAsync(string nome)
        {
            var filmes = await _ashowRepository.GetFilmesPorNomeAsync(nome);
            return mapper.Map<IEnumerable<FilmeModel>>(filmes);
        }
        public async Task<IEnumerable<FilmeModel>> GetFilmesAsync()
        {
            var filmes = await _ashowRepository.GetFilmesAsync();
            return mapper.Map<IEnumerable<FilmeModel>>(filmes);
        }
        public async Task<IEnumerable<SerieModel>> GetSeriesAsync()
        {
            var series = await _ashowRepository.GetSeriesAsync();
            return mapper.Map<IEnumerable<SerieModel>>(series);
        }
        public async Task<IEnumerable<GeneroModel>> GetGenerosAsync()
        {
            var generos = await _ashowRepository.GetGenerosAsync();
            return mapper.Map<IEnumerable<GeneroModel>>(generos);
        }
        public async Task<IEnumerable<FilmeModel>> GetFilmesPorGeneroAsync(int generoId)
        {
            var filmes = await _ashowRepository.GetFilmesPorGeneroAsync(generoId);
            return mapper.Map<IEnumerable<FilmeModel>>(filmes);
        }
        public async Task InserirFilmeAsync(FilmeModel filme)
        {
            filme.ValidaCampos();
            await _ashowRepository.InserirFilmeAsync(mapper.Map<FilmeEntity>(filme));
        }
        public async Task InserirSerieAsync(SerieModel serie)
        {
            serie.ValidaCampos();
            await _ashowRepository.InserirSerieAsync(mapper.Map<SerieEntity>(serie));
        }
        public async Task DeletarFilmeAsync(int filmeId)
        {
            if (filmeId.Equals(0))
            {
                throw new Exception("Informe um filme para exclusão");
            }

            await _ashowRepository.DeletarFilmeAsync(filmeId);
        }
        public async Task DeletarSerieAsync(int serieId)
        {
            if (serieId.Equals(0))
            {
                throw new Exception("Informe um série para exclusão");
            }

            await _ashowRepository.DeletarSerieAsync(serieId);
        }
        public async Task AtualizarFilmeAsync(FilmeModel filme)
        {
            filme.ValidaCampos(true);
            await _ashowRepository.AtualizarFilmeAsync(mapper.Map<FilmeEntity>(filme));
        }
        public async Task AtualizarSerieAsync(SerieModel serie)
        {
            serie.ValidaCampos(true);
            await _ashowRepository.AtualizarSerieAsync(mapper.Map<SerieEntity>(serie));
        }
    }
}
