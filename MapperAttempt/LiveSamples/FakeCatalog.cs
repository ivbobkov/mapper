using System.Collections.Generic;

namespace TinyMapper.Tests.LiveSamples
{
    public class FakeCatalog
    {
        public string CatalogId { get; set; }
        public string Name { get; set; }
        public string ToolClassId { get; set; }
        public string ToolGroupId { get; set; }
        public string CutGradeId { get; set; }
        public string CadId { get; set; }

        public virtual ICollection<FakeCatalogValue> CatalogValues { get; set; } = new List<FakeCatalogValue>();


        public bool Has(params string[] codes)
        {
            return true;
        }

        public bool Absence(params string[] codes)
        {
            return true;
        }

        public double? Double(string code)
        {
            return 0;
        }

        public bool? Boolean(string code)
        {
            return true;
        }

        public string String(string code)
        {
            return string.Empty;
        }
    }
}