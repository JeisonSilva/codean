using System.Numerics;
using codean.analisador.modelodados;

namespace codean.analisador.leitorarquivo
{
    public class LeitorLinha
    {
        private StreamReader stream;
        private OrganizadorCommits _organizador;

        private LeitorLinha(StreamReader stream)
        {
            this.stream = stream;
            _organizador = OrganizadorCommits.New();
        }

        internal static LeitorLinha New(StreamReader stream) => new LeitorLinha(stream);

        internal void ProximaLinha(Action<OrganizadorCommits> organizadorCommits, Action<List<Commit>> fimLeitura)
        {
            var linha = string.Empty;
            while ((linha = stream.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(linha))
                {
                    fimLeitura(_organizador.Commits.Value);
                    _organizador.Dispose();
                }
                else
                {
                    _organizador.AddLinha(linha, organizadorCommits);
                }
            }
        }
    }
}
