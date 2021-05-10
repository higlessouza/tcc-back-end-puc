namespace tcc_back_end_puc.Domain.Entities.Anuncios
{
    public class Avaliacao
    {
        public int Identificador { get; set; }
        public int IdentificadorAnuncio { get; set; }
        public string Nome { get; set; }
        public string Comentario { get; set; }
        public int Nota { get; set; }
    }
}
