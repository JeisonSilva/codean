namespace codean.analisador.modelodados
{
    public record Commit
    {
        public Commit(string linha)
        {
            var dados = linha.Split("--");

            Hash = dados[1];
            DataCommit = DateTime.Parse(dados[2]);
            NomeResponsavel = dados[3];
            Arquivos = new List<Arquivo>();

        }

        public string Hash { get; }
        public string NomeResponsavel { get; }
        public DateTime DataCommit { get; }
        public List<Arquivo> Arquivos { get; }

    }
}