namespace codean.analisador.modelodados
{
    public class OrganizadorCommits : IDisposable
    {
        private List<Commit> _commits;
        private string _novaLinha;
        private Commit _commit;

        private OrganizadorCommits()
        {
            _commits = new List<Commit>();
            Commits = new Lazy<List<Commit>>(_commits);
            _novaLinha = string.Empty;
        }

        public Lazy<List<Commit>> Commits { get; }


        internal static OrganizadorCommits New()
        {
            return new OrganizadorCommits();
        }

        internal void AddLinha(string novaLinha, Action<OrganizadorCommits> action)
        {
            _novaLinha = novaLinha;
            action(this);
        }

        internal OrganizadorCommits NovoCommit(Func<string, Commit> novoCommit)
        {
            if (_novaLinha.Contains("--"))
            {
                _commit = novoCommit(_novaLinha);
                _commits.Add(_commit);
            }
                

            return this;
        }

        internal OrganizadorCommits NovoArquivo(Func<string, Arquivo> novoArquivo)
        {
            if (!_novaLinha.Contains("--"))
            {
                if (_novaLinha.Contains(".css")
                    || _novaLinha.Contains(".js")
                    || _novaLinha.Contains(".png")
                    || _novaLinha.Contains(".yaml")
                    || _novaLinha.Contains(".yml")
                    || _novaLinha.Contains(".Reference.")
                    || _novaLinha.Contains(".wsdl")
                    || _novaLinha.Contains(".html")
                    || _novaLinha.Contains(".sqlproj")
                    || _novaLinha.Contains(".xsd")
                    || _novaLinha.Contains(".vbproj")
                    || _novaLinha.Contains(".svg")
                    || _novaLinha.Contains(".sqlplan")
                    || _novaLinha.Contains(".ai")
                    || _novaLinha.Contains(".xml")
                    || _novaLinha.Contains(".bak")
                    || _novaLinha.Contains(".txt")
                    )
                    return this;

                var arquivo = novoArquivo(_novaLinha);

                if(_commit != null)
                    _commits.FirstOrDefault(x => x.Hash == _commit.Hash)?.Arquivos.Add(arquivo);
            }


            return this;
        }

        public void Dispose()
        {
            _commits.Clear();
        }
    }
}
