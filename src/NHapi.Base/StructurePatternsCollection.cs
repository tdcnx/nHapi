
namespace NHapi.Base
{
    using System.Collections;

    internal class StructurePatternsCollection
    {
        private static readonly StructurePatternsCollection InstanceValue = new StructurePatternsCollection();

        private readonly Hashtable map = new Hashtable();

        static StructurePatternsCollection()
        {
        }

        private StructurePatternsCollection()
        {
            var loader = new StructurePatternsLoader(map);
            loader.Load();
        }

        public static StructurePatternsCollection Instance => InstanceValue;

        public Hashtable Maps => map;

    }
}
