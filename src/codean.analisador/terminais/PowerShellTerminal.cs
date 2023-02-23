using codean.analisador.leitorarquivo;
using System.Management.Automation;

namespace codean.analisador.terminais
{
    public class PowerShellTerminal : ICommandTerminal
    {
        private readonly PowerShell _powerShell;
        private FileStream _file;
        private StreamReader _stream;

        private PowerShellTerminal(PowerShell powerShell)
        {
            _powerShell = powerShell;
        }

        public void AddScript(string script)
            => _powerShell.AddScript(script);

        public static ICommandTerminal Create()
            => new PowerShellTerminal(PowerShell.Create());

        public FileGitLog CreateStream(string path)
        {
            _file = File.OpenRead(path);
            _stream = new StreamReader(_file);
            return new FileGitLog(_stream);
        }

        public void DeleteDirectory(string path)
            => Directory.Delete(path);

        public void DeleteFile(string pach)
            => File.Delete(pach);

        public void Dispose()
        {
            _file?.Dispose();
            _stream?.Dispose();
        }

        public bool ExistsDirectory(string path)
            => Directory.Exists(path);

        public void Invoke()
            => _powerShell.Invoke();

        public bool IsCreatedFile(string path)
            => File.Exists(path);
    }
}
