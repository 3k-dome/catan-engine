namespace CatanLib.Interfaces
{
    public interface IVectorizableComponent
    {
        public IEnumerable<float> ToVector();
        public IEnumerable<string> ToExplainedVector() => throw new NotImplementedException();
    }
}
