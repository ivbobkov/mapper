using System.Collections.Generic;

namespace SampleMapper.TDM
{
    public class CatalogItem
    {
        public string CatalogId { get; set; }
        public string Name { get; set; }
        public string ToolClassId { get; set; }
        public string ToolGroupId { get; set; }
        public string CutGradeId { get; set; }
        public string CadId { get; set; }

        public virtual ICollection<CatalogValue> CatalogValues { get; set; } = new List<CatalogValue>();

        public bool HasParameter(string parameterName)
        {
            return true;
        }
    }
}