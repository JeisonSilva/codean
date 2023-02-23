using codean.analisador.leitorarquivo;

namespace codean.analisador.terminais
{
    public interface ICommandTerminal : IDisposable
    {
        void AddScript(string script);
        IFileGitLog CreateStream(string path);
        void DeleteDirectory(string path);
        void DeleteFile(string pach);
        bool ExistsDirectory(string path);
        void Invoke();
        bool IsCreatedFile(string path);
    }
}
