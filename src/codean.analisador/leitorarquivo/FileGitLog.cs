namespace codean.analisador.leitorarquivo
{
    public class FileGitLog : IFileGitLog
    {
        private StreamReader stream;

        public FileGitLog(StreamReader stream)
        {
            this.stream = stream;
        }

        public virtual IEnumerable<string> ReadLine { get {
                while (!this.stream.EndOfStream)
                {
                    var linha = string.Empty;
                    if (!string.IsNullOrEmpty((linha = this.stream.ReadLine())))
                        yield return linha;
                }    
            
                yield break;
            } 
        
        }
    }
}
