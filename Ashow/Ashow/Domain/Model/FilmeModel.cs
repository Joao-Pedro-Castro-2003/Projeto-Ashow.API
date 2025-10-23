namespace Ashow.Domain.Model
{
    public class FilmeModel
    {
        public int FilmeId { get; set; }
        public string? Nome { get; set; }
        public GeneroModel Genero { get; set; }
        public int AnoLancamento { get; set; }
        public string? Sinopse { get; set; }
        public int ClassificacaoIndicativa { get; set; }
        public string? Diretor { get; set; }
        public int TempoDuracao { get; set; }
        public void ValidaCampos(bool isAtualizacao = false)
        {
            if (FilmeId.Equals(0) && isAtualizacao)
            {
                throw new Exception("Infome um filme para atualizar");
            }
            if (string.IsNullOrEmpty(Nome))
            {
                throw new Exception("Título é obrigatório");
            }
            if (Genero.GeneroId.Equals(0))
            {
                throw new Exception("Gênero é obrigatório");
            }
            if (AnoLancamento.Equals(0))
            {
                throw new Exception("Ano de lançamento é obrigatório");
            }
            if (AnoLancamento.ToString().Length < 4 || AnoLancamento > DateTime.Now.Year)
            {
                throw new Exception("Ano de lançamento inválido");
            }
            if (string.IsNullOrEmpty(Sinopse))
            {
                throw new Exception("Sinopse é obrigatório");
            }
            if (ClassificacaoIndicativa.Equals(0))
            {
                throw new Exception("Classificação Indicativa é obrigatório");
            }
            if (string.IsNullOrEmpty(Diretor))
            {
                throw new Exception("Diretor é obrigatório");
            }
            if (TempoDuracao.Equals(0))
            {
                throw new Exception("Duração é obrigatório");
            }
        }
    }
}
