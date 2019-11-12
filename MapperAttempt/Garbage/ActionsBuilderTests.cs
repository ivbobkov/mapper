using NUnit.Framework;

namespace TinyMapper.Tests.Garbage
{
    [TestFixture]
    public class ActionsBuilderTests
    {
        //[Test]
        //public void Execute_NonConditionalAction_ReturnShouldBeExecuted()
        //{
        //    const int expectedValue = 2;
        //    var subject = Subject<int, FakeSubject>()
        //        .For(x => x.TotalLength, x => x.Assign(c => c));

        //    var result = subject.Build().Single().Execute(new FakeSubject(), expectedValue);

        //    Assert.AreEqual(expectedValue, result.TotalLength);
        //}

        //[Test]
        //public void Execute_ConditionalAction_IfStatementShouldBeExecuted()
        //{
        //    const int expectedValue = 2;
        //    var subject = Subject<int, FakeSubject>()
        //        .For(x => x.TotalLength, x => x.If(c => true).Assign(c => c).Else.Assign(-100));

        //    var result = subject.Build().Single().Execute(new FakeSubject(), expectedValue);

        //    Assert.AreEqual(2, result.TotalLength);
        //}

        //[Test]
        //public void Execute_ConditionalAction_ElseStatementShouldBeExecuted()
        //{
        //    const int expectedValue = 2;
        //    var actionsBuilder = Subject<int, FakeSubject>()
        //        .For(x => x.TotalLength, x => x.If(c => false).Assign(-100).Else.Assign(c => c));

        //    var result = actionsBuilder.Build().Single().Execute(new FakeSubject(), expectedValue);

        //    Assert.AreEqual(expectedValue, result.TotalLength);
        //}

        //[Test]
        //public void Execute_ConditionalAction_NestedIfStatementShouldBeExecuted()
        //{
        //    const int expectedValue = 2;
        //    var actionsBuilder = Subject<int, FakeSubject>()
        //        .For(x => x.TotalLength, x => x
        //            .If(c => false).Assign(-100)
        //            .Else.If(c => true).Assign(c => c)
        //            .Else.Assign(-200));

        //    var result = actionsBuilder.Build().Single().Execute(new FakeSubject(), expectedValue);

        //    Assert.AreEqual(expectedValue, result.TotalLength);
        //}

        //[Test]
        //public void Execute_ConditionalAction_NestedElseStatementShouldBeExecuted()
        //{
        //    const int expectedValue = 2;
        //    var actionsBuilder = Subject<int, FakeSubject>()
        //        .For(x => x.TotalLength, x => x
        //            .If(c => false).Return(-100)
        //            .Else.If(c => false).Return(-200)
        //            .Else.Assign(c => c));

        //    var result = actionsBuilder.Build().Single().Execute(new FakeSubject(), expectedValue);

        //    Assert.AreEqual(expectedValue, result.TotalLength);
        //}

        //private class FakeSubject
        //{
        //    public double TotalLength { get; set; }
        //}

        //private ActionsBuilder<TSource, TReceiver> Subject<TSource, TReceiver>() => new ActionsBuilder<TSource, TReceiver>();
    }
}