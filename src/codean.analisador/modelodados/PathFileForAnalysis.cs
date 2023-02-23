namespace codean.analisador.modelodados
{
    public struct PathFileForAnalysis
    {
        private readonly string _path;

        public PathFileForAnalysis()
        {
            _path = @$"{Directory.GetCurrentDirectory()}\logfile.log";
        }

        public PathFileForAnalysis(string path)
        {
            _path = path;
        }

        public static implicit operator string(PathFileForAnalysis path)
            => path._path;
    }
}
