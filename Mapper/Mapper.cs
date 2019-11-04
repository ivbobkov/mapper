namespace TinyMapper
{
    public class Mapper : IMapper
    {
        private readonly IMappingProvider _mappingProvider;

        public Mapper(IMappingProvider mappingProvider)
        {
            _mappingProvider = mappingProvider;
        }

        public TReceiver Map<TSource, TReceiver>(TSource source)
        {
            var mappingFunc = _mappingProvider.GetTypedMappingFunc<TSource, TReceiver>();

            return mappingFunc(source);
        }
    }
}