
namespace NHapi.Base
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal class MapResourceLoader
    {
        private readonly Hashtable map;
        private readonly string separator;

        public MapResourceLoader(Hashtable map, string separator = " \t")
        {
            this.map = map;
            this.separator = separator;
        }

        public void Load()
        {
            var packages = GetPackages();
            foreach (var package in packages)
            {
                Assembly assembly = null;
                try
                {
                    var assemblyToLoad = RemoveTrailingDot(package);
                    assembly = Assembly.Load(assemblyToLoad);
                }
                catch (FileNotFoundException)
                {
                    // Just skip, this assembly is not used
                }

                if (assembly != null)
                {
                  LoadResourceFromPackage(assembly, package);
                }
            }
        }

        protected virtual IList<Hl7Package> GetPackages()
        {
            return PackageManager.Instance.Packages;
        }

        protected virtual void LoadResourceFromPackage(Assembly assembly, Hl7Package package)
        {
            using (var inResource = assembly.GetManifestResourceStream(GetResourceName(package)))
            {
                var structures = new NameValueCollection();
                if (inResource != null)
                {
                    var splitSeparators = separator.ToArray();
                    using (var sr = new StreamReader(inResource))
                    {
                        var line = sr.ReadLine();
                        while (line != null)
                        {
                            if ((line.Length > 0) && (line[0] != '#'))
                            {
                                var lineElements = line.Split(splitSeparators);
                                structures.Add(lineElements[0], lineElements[1]);
                            }

                            line = sr.ReadLine();
                        }
                    }
                }

                map[package.Version] = structures;
            }
        }

        protected virtual string GetResourceName(Hl7Package package)
        {
            return package.EventMappingResourceName;
        }

        private static string RemoveTrailingDot(Hl7Package package)
        {
            var assemblyString = package.PackageName;
            var lastChar = assemblyString.LastOrDefault();
            var trailingDot = lastChar != default(char) && lastChar.ToString() == ".";
            if (trailingDot)
            {
                assemblyString = assemblyString.Substring(0, assemblyString.Length - 1);
            }

            return assemblyString;
        }
    }
}
