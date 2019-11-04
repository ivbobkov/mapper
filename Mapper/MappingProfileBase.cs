using TinyMapper.Builders;

namespace TinyMapper
{
    public abstract class MappingProfileBase
    {
        protected IMappingBuilder<TSource, TReceiver> DefineMapping<TSource, TReceiver>()
        {
            //var mappingBuilder = new MappingProfile<TSource, TReceiver>();

            return null;
        }
    }
}