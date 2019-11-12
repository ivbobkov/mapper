using System.Linq.Expressions;

namespace SampleMapper
{
    public interface ICondition
    {
        LambdaExpression ToExpression();
    }
}