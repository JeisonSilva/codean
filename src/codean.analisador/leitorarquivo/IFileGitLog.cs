namespace codean.analisador.leitorarquivo
{
    public interface IFileGitLog
    {
        IEnumerable<string> ReadLine { get; }
    }
}
