using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enderlook.Extensions
{
    public static class AssemblyExtensions
    {
        public static (IEnumerable<AssemblyName> references, IEnumerable<AssemblyName> errors) GetReferencedAssemblyNamesRecursive(this Assembly assembly)
        {
            List<AssemblyName> referencedAssemblies = new List<AssemblyName>();
            List<AssemblyName> errorAssemblies = new List<AssemblyName>();

            Stack<Assembly> toLookUpAssemblies = new Stack<Assembly>();
            toLookUpAssemblies.Push(assembly);

            while (toLookUpAssemblies.TryPop(out Assembly result))
            {
                foreach (AssemblyName referencedName in result.GetReferencedAssemblies().OrderByDescending(e => e.Version))
                {
                    try
                    {
                        Assembly loadedAssembly = Assembly.ReflectionOnlyLoad(referencedName.FullName);
                        referencedAssemblies.Add(referencedName);
                        toLookUpAssemblies.Push(loadedAssembly);
                    }
                    catch (Exception)
                    {
                        errorAssemblies.Add(referencedName);
                    }
                }
            }

            return (referencedAssemblies, errorAssemblies);
        }
    }
}
