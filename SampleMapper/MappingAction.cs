namespace SampleMapper
{
    public class MappingAction<TSource, TReceiverMember>
    {
        public Clause<TSource> ExecutionClause { get; set; }
        public ValueResolver<TSource, TReceiverMember> ValueResolver { get; set; }
    }
}