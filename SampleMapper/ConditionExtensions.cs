namespace SampleMapper
{
    public static class ConditionExtensions
    {
        public static Condition<TSource> And<TSource>(this Condition<TSource> source, Condition<TSource> condition)
        {
            return new AndCondition<TSource>(source, condition);
        }

        public static Condition<TSource> Or<TSource>(this Condition<TSource> source, Condition<TSource> condition)
        {
            return new OrCondition<TSource>(source, condition);
        }

        public static Condition<TSource> Not<TSource>(this Condition<TSource> source)
        {
            return new NotCondition<TSource>(source);
        }
    }
}