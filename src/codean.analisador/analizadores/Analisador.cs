using codean.analisador.leitorarquivo;
using codean.analisador.modelodados;
using System.IO;

namespace codean.analisador.analizadores
{
    public class Analisador
    {
        private readonly List<Commit> _commits;
        private IEnumerable<Ranking> _ranking;

        public static AnalisadorBuilder Instance(terminais.ICommandTerminal commandTerminal)
            => new AnalisadorBuilder(new Analisador(), commandTerminal);


        private Analisador()
        {
            _commits = new List<Commit>();
            Commits = new(_commits);
            _ranking = new List<Ranking>(10);

        }

        public Lazy<List<Commit>> Commits { get; }

        public void AnalizarArquivo(ArquivoDadosGitLog arquivoDadosGitLog)
        {
            if (arquivoDadosGitLog == null)
                return;

            arquivoDadosGitLog.Open(leitor =>
            {
                leitor.ProximaLinha(organizador =>
                {
                    organizador
                            .NovoCommit((linha) => new Commit(linha))
                            .NovoArquivo((linha) => new Arquivo(linha));

                }, fimLeitura: (commits) =>
                {
                    _commits.AddRange(commits);
                });
            });
        }

        public Analisador ProcessarTotalAlteracoesPorArquivo()
        {
            _ranking = AgruparArquivos(Commits.Value.Select(x => x.Arquivos)).Select(a => {
                return new Ranking(nome: a.Key, total: a.Sum(x => x.Total));
            });

            return this;

        }

        public Analisador ProcessarTotalAlteracoesPorExtencao(ExtencaoArquivo extencao)
        {
            var rankings = new List<Ranking>();
            var arquivos = Commits.Value.Select(x => x.Arquivos);
            var arquivosAgrupados = AgruparArquivos(arquivos);

            _ranking = AgruparArquivos(Commits.Value.Select(x => x.Arquivos)).Select(a => {
                return new Ranking(nome: a.Key, total: a.Sum(x => x.Total));
                
            }).Where(x=>x.Nome.Contains($".{extencao.ToString().ToLower()}"));

            return this;
        }

        public IEnumerable<Ranking> ExibirEmOrderDecrescenteOsTop(Top top)
        {
            switch (top)
            {
                case Top.Top3:
                    return ObterOsTresMaioresArquivosAlterados(_ranking);
                case Top.Top10:
                    return ObterOsDezMaioresArquivosAlterados(_ranking);
                default:
                    return ObterOsTresMaioresArquivosAlterados(_ranking);
            }
        }

        private static IEnumerable<Ranking> ObterOsDezMaioresArquivosAlterados(IEnumerable<Ranking> _ranking)
        {
            return _ranking.OrderByDescending(x => x.Total).Take(10);
        }

        private static IEnumerable<Ranking> ObterOsTresMaioresArquivosAlterados(IEnumerable<Ranking> _ranking)
        {
            return _ranking.OrderByDescending(x => x.Total).Take(3);
        }

        static IEnumerable<IGrouping<string, Arquivo>> AgruparArquivos(IEnumerable<List<Arquivo>> arquivos)
        {
            return arquivos.SelectMany(l => l).GroupBy(x => x.Nome);
        }

        
    }
}
