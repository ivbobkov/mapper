using System;
using TinyMapper.Core;

namespace TinyMapper.Tests.LiveSamples.Test
{
    public class ClassGroupClause : IClause<FakeCatalog, ClassGroupClause>
    {
        public string Class { get; private set; }
        public string Group { get; private set; }

        public ClassGroupClause(string @class, string @group)
        {
            Class = @class;
            Group = @group;
        }

        public bool Equals(ClassGroupClause other)
        {
            throw new NotImplementedException();
        }

        public Func<FakeCatalog, bool> Compile()
        {
            throw new NotImplementedException();
        }
    }
}