namespace codean.analisador.modelodados
{
    public record Arquivo
    {
        public Arquivo(string linha)
        {
            var dados = linha.Split('\t');

            if (int.TryParse(dados[0], out int f))
            {
                TotalNovasFuncionalidades = f;
            }
            else
            {
                TotalNovasFuncionalidades = 1;
            }

            if (int.TryParse(dados[1], out int a))
            {
                TotalAlteracoes = a;
            }
            else
            {
                TotalAlteracoes = 0;
            }

            Nome = dados[2];
            Total = TotalNovasFuncionalidades + TotalAlteracoes;
        }

        public string Nome { get; }
        public int TotalNovasFuncionalidades { get; }
        public int TotalAlteracoes { get; }
        public int Total { get; }
    }
}