namespace SampleMapper
{
    public class MappingAction
    {
        public MappingAction(ICondition executionClause, IValueResolver valueResolver)
        {
            ExecutionClause = executionClause;
            ValueResolver = valueResolver;
        }

        public ICondition ExecutionClause { get; }
        public IValueResolver ValueResolver { get; }
    }
}