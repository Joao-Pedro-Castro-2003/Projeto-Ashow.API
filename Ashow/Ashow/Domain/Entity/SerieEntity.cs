using Ashow.Domain.Model;

namespace Ashow.Domain.Entity
{
    public class SerieEntity
    {
        public int SerieId { get; set; }
        public string? Nome { get; set; }
        public GeneroEntity Genero { get; set; }
        public int AnoLancamento { get; set; }
        public string? Sinopse { get; set; }
        public int ClassificacaoIndicativa { get; set; }
        public int QtdeEpisodios { get; set; }
    }
}
