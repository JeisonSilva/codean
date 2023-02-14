namespace codean.analisador
{
    public record Arquivo
    {
        public Arquivo(string linha)
        {
            var dados = linha.Split('\t');

            if(int.TryParse(dados[0], out int f))
            {
                this.TotalNovasFuncionalidades = f;
            }
            else
            {
                this.TotalNovasFuncionalidades = 1;
            }

            if (int.TryParse(dados[1], out int a))
            {
                this.TotalAlteracoes = a;
            }
            else
            {
                this.TotalAlteracoes = 0;
            }
            
            this.Nome = dados[2];
            this.Total = this.TotalNovasFuncionalidades + this.TotalAlteracoes;
        }

        public string Nome { get; }
        public int TotalNovasFuncionalidades { get; }
        public int TotalAlteracoes { get; }
        public int Total { get; }
    }
}