using System;
using TinyMapper.Builders;

namespace TinyMapper
{
    public abstract class MappingProfileBase<TSource, TReceiver>
    {
        public IMapping<TSource, TReceiver> Build()
        {
            throw new NotImplementedException();
        }

        protected IMappingBuilder<TSource, TReceiver> DefineMapping()
        {
            var mappingBuilder = new MappingBuilder<TSource, TReceiver>();

            return mappingBuilder;
        }
    }
}