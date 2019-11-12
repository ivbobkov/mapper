namespace SampleMapper
{
    public class MemberMap
    {
        public MemberMap(ICondition condition, IValueResolver valueResolver)
        {
            Condition = condition;
            ValueResolver = valueResolver;
        }

        public ICondition Condition { get; }
        public IValueResolver ValueResolver { get; }
    }
}