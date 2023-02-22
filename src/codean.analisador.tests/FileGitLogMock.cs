using codean.analisador.leitorarquivo;
using System.Text;

namespace codean.analisador.tests
{
    public class FileGitLogMock : FileGitLog
    {
        private StringBuilder stringBuilder;

        public FileGitLogMock(StringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
        }

        public override IEnumerable<string> ReadLine
        {
            get
            {
                var lista = this.stringBuilder.ToString().Split(';');
                for (int i = 0; i < lista.Length; i++)
                {
                    yield return lista[i];
                }

                yield return string.Empty;
            }
        }
    }
}
