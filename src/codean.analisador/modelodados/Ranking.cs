namespace codean.analisador.modelodados
{
    public class Ranking
    {
        public Ranking(string nome, int total)
        {
            Nome = nome;
            Total = total;
        }

        public string Nome { get; }
        public int Total { get; }
    }
}
