namespace SampleMapper
{
    public class Mapper : IMapper
    {
        private readonly IMapperConfiguration _mapperConfiguration;

        public Mapper(IMapperConfiguration mapperConfiguration)
        {
            _mapperConfiguration = mapperConfiguration;
        }

        public TReceiver Map<TSource, TReceiver>(TSource source)
        {
            var mapperFunc = _mapperConfiguration.GetMapperFunc<TSource, TReceiver>(source);

            return mapperFunc(source);
        }
    }
}