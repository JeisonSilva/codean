namespace codean.analisador.leitorarquivo
{
    public class FileGitLog
    {
        private StreamReader stream;

        public FileGitLog(StreamReader stream)
        {
            this.stream = stream;
        }

        public virtual IEnumerable<string> ReadLine { get {
                var linha = string.Empty;
                while (!string.IsNullOrEmpty((linha = this.stream.ReadLine())))
                {
                    yield return linha;
                }    
            
                yield break;
            } 
        
        }
    }
}
