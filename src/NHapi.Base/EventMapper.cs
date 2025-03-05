namespace NHapi.Base
{
    using System.Collections;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal class EventMapper
    {
        private static readonly EventMapper InstanceValue = new EventMapper();

        private readonly Hashtable map = new Hashtable();

        static EventMapper()
        {
        }

        private EventMapper()
        {
            var loader = new MapResourceLoader(map);
            loader.Load();
        }

        public static EventMapper Instance => InstanceValue;

        public Hashtable Maps => map;

    }
}