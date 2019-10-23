using System;
using System.Linq;
using System.Linq.Expressions;

namespace TinyMapper
{
    public class AssignAction<TSource, TReceiver, TReceiverProperty> : IAction<TSource, TReceiver>
    {
        public Expression<Func<TSource, TReceiverProperty>> SourceSelector { get; }
        private Expression<Func<TReceiver, TReceiverProperty>> ReceiverSelector { get; }

        public AssignAction(
            Expression<Func<TReceiver, TReceiverProperty>> receiverSelector,
            Expression<Func<TSource, TReceiverProperty>> sourceSelector)
        {
            SourceSelector = sourceSelector;
            ReceiverSelector = receiverSelector;
        }

        public TReceiver Execute(TReceiver receiver, TSource source)
        {
            return AssignNewValue(receiver, source);
        }

        // говно блядь, переписать к хуйям
        private TReceiver AssignNewValue(TReceiver destination, TSource source)
        {
            ParameterExpression valueParameterExpression = Expression.Parameter(typeof(TReceiverProperty));
            Expression targetExpression = ReceiverSelector.Body
                is UnaryExpression ? ((UnaryExpression)ReceiverSelector.Body).Operand : ReceiverSelector.Body;

            var assign = Expression.Lambda<Action<TReceiver, TReceiverProperty>>
            (
                Expression.Assign(targetExpression, Expression.Convert(valueParameterExpression, targetExpression.Type)),
                ReceiverSelector.Parameters.Single(),
                valueParameterExpression
            );

            assign.Compile().Invoke(destination, SourceSelector.Compile()(source));

            return destination;
        }
    }
}