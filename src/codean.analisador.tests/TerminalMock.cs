using Bogus;
using codean.analisador.leitorarquivo;
using codean.analisador.terminais;
using System.Text;

namespace codean.analisador.tests
{
    public class TerminalMock : ICommandTerminal
    {
        public TerminalMock()
        {

        }

        public void AddScript(string script)
        {
            
        }

        public IFileGitLog CreateStream(string path)
        {
            return new Faker<IFileGitLog>("pt_BR").CustomInstantiator(f => {
                return new FileGitLogMock(new StringBuilder()
                .Append("--e342b8e--2022-10-11--dependabot[bot];")
                .Append(@"1	1	src/CSharp/CodeCracker/Design/StaticConstructorExceptionCodeFixProvider.cs"));
            }).Generate();
        }

        public void DeleteDirectory(string path)
        {
            
        }

        public void DeleteFile(string pach)
        {
            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool ExistsDirectory(string path)
        {
            return false;
        }

        public void Invoke()
        {
            
        }

        public bool IsCreatedFile(string path)
        {
            return true;
        }
    }
}
