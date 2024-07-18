namespace Governement_Public_Health_Care.NewFolder
{
    public interface ITransformation<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        public Task<TTarget> TransformingData(TSource entity);
    }
}
