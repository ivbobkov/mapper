using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public interface ICondition : IEquatable<ICondition>
    {
        LambdaExpression AsLambda();
        int GetHashCode();
    }
}