namespace SampleMapper
{
    public class ValueResolvingAction
    {
        public ValueResolvingAction(ICondition condition, IValueResolver valueResolver)
        {
            Condition = condition;
            ValueResolver = valueResolver;
        }

        public ICondition Condition { get; }
        public IValueResolver ValueResolver { get; }
    }
}