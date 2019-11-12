using System.Linq.Expressions;

namespace SampleMapper
{
    public interface IValueResolver
    {
        LambdaExpression ToLambda();
    }
}