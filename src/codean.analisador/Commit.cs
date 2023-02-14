namespace codean.analisador
{
    public record Commit
    {
        public Commit(string linha)
        {
            var dados = linha.Split("--");

            this.Hash = dados[1];
            this.DataCommit = DateTime.Parse(dados[2]);
            this.NomeResponsavel = dados[3];
            this.Arquivos = new List<Arquivo>();

        }

        public string Hash { get;}
        public string NomeResponsavel { get;}
        public DateTime DataCommit { get;}
        public List<Arquivo> Arquivos { get;}

    }
}