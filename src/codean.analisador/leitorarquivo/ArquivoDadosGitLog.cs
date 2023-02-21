namespace codean.analisador.leitorarquivo
{
    public class ArquivoDadosGitLog
    {
        private string path;

        public ArquivoDadosGitLog(string path)
        {
            this.path = path;
        }

        public static ArquivoDadosGitLog New(string path)
        {
            return new ArquivoDadosGitLog(path);
        }
        public void Open(Action<LeitorLinha> action)
        {
            if (File.Exists(path))
            {
                using var file = File.OpenRead(path);
                using var stream = new StreamReader(file);
                action(LeitorLinha.New(stream));
            }
            else
            {
                throw new FileNotFoundException("Arquivo não encontrado! Execute o procedimento novamente");
            }

        }
    }
}
