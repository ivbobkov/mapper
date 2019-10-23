using System;
using System.Linq.Expressions;

namespace TinyMapper.Builders
{
    public class ActionBuilder<TSource, TReceiver, TReceiverProperty> :
        IActionBuilder<TSource, TReceiver, TReceiverProperty>
    {
        private readonly Expression<Func<TReceiver, TReceiverProperty>> _receiverSelector;

        public ActionBuilder(Expression<Func<TReceiver, TReceiverProperty>> receiverSelector)
        {
            _receiverSelector = receiverSelector;
        }

        public ICanAddResolve<TSource, TReceiver> Assign(
            Expression<Func<TSource, TReceiverProperty>> statement,
            Expression<Func<TReceiverProperty, TReceiverProperty>> converter)
        {
            throw new NotImplementedException();
        }

        public ICanAddResolve<TSource, TReceiver> Assign(TReceiverProperty value)
        {
            throw new NotImplementedException();
        }

        public ICanAddAssign<TSource, TReceiver, TReceiverProperty> If(Expression<Func<TSource, bool>> clause)
        {
            throw new NotImplementedException();
        }

        private class AssignBuilder : ICanAddResolve<TSource, TReceiver>
        {
            private readonly Expression<Func<TReceiver, TReceiverProperty>> _receiverSelector;

            public AssignBuilder(Expression<Func<TReceiver, TReceiverProperty>> receiverSelector)
            {
                _receiverSelector = receiverSelector;
            }

            public IAction<TSource, TReceiver> Resolve()
            {
                throw new NotImplementedException();
            }
        }

        private class IfElseBuilder :
            ICanAddAssign<TSource, TReceiver, TReceiverProperty>,
            ICanAddIfOrAssign<TSource, TReceiver, TReceiverProperty>,
            ICanAddElse<TSource, TReceiver, TReceiverProperty>,
            ICanAddResolve<TSource, TReceiver>
        {
            public ICanAddAssign<TSource, TReceiver, TReceiverProperty> If(Expression<Func<TSource, bool>> clause)
            {
                throw new NotImplementedException();
            }

            ICanAddResolve<TSource, TReceiver> ICanAddIfOrAssign<TSource, TReceiver, TReceiverProperty>.Assign(
                Expression<Func<TSource, TReceiverProperty>> statement)
            {
                throw new NotImplementedException();
            }

            ICanAddResolve<TSource, TReceiver> ICanAddIfOrAssign<TSource, TReceiver, TReceiverProperty>.Assign(
                Expression<Func<TSource, TReceiverProperty>> statement,
                Expression<Func<TReceiverProperty, TReceiverProperty>> converter)
            {
                throw new NotImplementedException();
            }

            ICanAddResolve<TSource, TReceiver> ICanAddIfOrAssign<TSource, TReceiver, TReceiverProperty>.Assign(
                TReceiverProperty value)
            {
                throw new NotImplementedException();
            }

            ICanAddElse<TSource, TReceiver, TReceiverProperty> ICanAddAssign<TSource, TReceiver, TReceiverProperty>.
                Assign(Expression<Func<TSource, TReceiverProperty>> statement)
            {
                throw new NotImplementedException();
            }

            ICanAddElse<TSource, TReceiver, TReceiverProperty> ICanAddAssign<TSource, TReceiver, TReceiverProperty>.
                Assign(Expression<Func<TSource, TReceiverProperty>> statement,
                    Expression<Func<TReceiverProperty, TReceiverProperty>> converter)
            {
                throw new NotImplementedException();
            }

            ICanAddElse<TSource, TReceiver, TReceiverProperty> ICanAddAssign<TSource, TReceiver, TReceiverProperty>.
                Assign(TReceiverProperty value)
            {
                throw new NotImplementedException();
            }

            ICanAddIfOrAssign<TSource, TReceiver, TReceiverProperty> ICanAddElse<TSource, TReceiver, TReceiverProperty>.
                Else { get; }

            IAction<TSource, TReceiver> ICanAddResolve<TSource, TReceiver>.Resolve()
            {
                throw new NotImplementedException();
            }
        }
    }
}