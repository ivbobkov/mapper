namespace TinyMapper
{
    public class Mapper<TSource, TReceiver> : IMapper<TSource, TReceiver>
    {
        private readonly IMappingProvider<TSource, TReceiver> _mappingProvider;

        public Mapper(IMappingProvider<TSource, TReceiver> mappingProvider)
        {
            _mappingProvider = mappingProvider;
        }

        public TReceiver Map(TSource source)
        {
            throw new System.NotImplementedException();
        }
    }
}