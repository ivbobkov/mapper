using System.Collections.Generic;

namespace SampleMapper.Tests.Mapper.Fakes
{
    public class FakeSource
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }

        public IEnumerable<FakeSourceItem> Items { get; set; }
    }
}