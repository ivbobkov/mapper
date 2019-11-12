using System;
using System.Collections.Generic;

namespace SampleMapper.Configurators
{
    public class MemberMapsBuilder<TSource, TReceiverMember> :
        IMemberMapsBuilder<TSource, TReceiverMember>,
        ICanAddDo<TSource, TReceiverMember>
    {
        public List<MemberMap> MemberMaps { get; } = new List<MemberMap>();

        private Condition<TSource> _condition;
        private ValueResolver<TSource, TReceiverMember> _resolver;

        void IMemberMapsBuilder<TSource, TReceiverMember>.Do(ValueResolver<TSource, TReceiverMember> resolver)
        {
            if (_condition != null)
            {
                throw new InvalidOperationException();
            }

            if (_resolver != null)
            {
                throw new InvalidOperationException();
            }

            MemberMaps.Add(new MemberMap(new BlankCondition<TSource>(), resolver));
        }

        ICanAddDo<TSource, TReceiverMember> IMemberMapsBuilder<TSource, TReceiverMember>.If(Condition<TSource> condition)
        {
            if (_resolver != null)
            {
                throw new InvalidOperationException();
            }

            _condition = condition;

            return this;
        }

        void ICanAddDo<TSource, TReceiverMember>.Do(ValueResolver<TSource, TReceiverMember> resolver)
        {
            MemberMaps.Add(new MemberMap(_condition, resolver));
            _condition = null;
            _resolver = null;
        }
    }
}