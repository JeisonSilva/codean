namespace codean.analisador
{
    public class LogGit
    {
        private string path;

        public LogGit(string path)
        {
            this.path = path;
        }

        public static LogGit New(string path)
        {
            return new LogGit(path);
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
