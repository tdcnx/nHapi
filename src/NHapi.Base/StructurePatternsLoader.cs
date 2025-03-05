
namespace NHapi.Base
{
    using System.Collections;

    internal class StructurePatternsLoader : MapResourceLoader
    {
        public StructurePatternsLoader(Hashtable map)
            : base(map, "\t")
        {
        }

        protected override string GetResourceName(Hl7Package package)
        {
            var packageName = package.PackageName;
            if (!package.PackageName.EndsWith("."))
            {
                packageName = packageName + ".";
            }

            return $"{packageName}StructureMapping.StructurePatternMap.properties";
        }
    }
}
