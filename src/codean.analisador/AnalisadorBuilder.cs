using codean.analisador.analizadores;
using codean.analisador.leitorarquivo;
using codean.analisador.modelodados;
using codean.analisador.terminais;

namespace codean.analisador
{
    public class AnalisadorBuilder
    {
        private readonly ICommandTerminal _powerShellTerminal;
        private readonly Analisador _analisador;

        public AnalisadorBuilder(Analisador analisador, ICommandTerminal powerShellTerminal)
        {
            _powerShellTerminal = powerShellTerminal;
            _analisador = analisador;
        }


        public AnalisadorBuilder AnalizarArquivo(PathFileForAnalysis path)
        {
            var gitlog = GitLog.GerarArquivoDosCommitsPorPeriodo(_powerShellTerminal ,(gitlog) =>{
                return gitlog.GerarArquivoDadosCommit(path);
            });


            _analisador.AnalizarArquivo(gitlog);
            return this;
        }


        public Analisador Construir()
        {            
            return _analisador;
        }
    }


}
