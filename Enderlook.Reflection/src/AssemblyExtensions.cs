using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enderlook.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="Assembly"/>.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Get the name of all referenced assemblies recursively.
        /// </summary>
        /// <param name="assembly">Root <see cref="Assembly"/>.</param>
        /// <param name="references">Referenced <see cref="AssemblyName"/>.</param>
        /// <param name="errors"><see cref="AssemblyName"/> which could not be loaded.</param>
        /// <returns>Whenever all <see cref="AssemblyName"/> could be loaded without error.</returns>
        public static bool GetReferencedAssemblyNamesRecursive(this Assembly assembly, out IEnumerable<AssemblyName> references, out IEnumerable<AssemblyName> errors)
        {
            List<AssemblyName> _referencedAssemblies = new List<AssemblyName>();
            List<AssemblyName> _errorAssemblies = new List<AssemblyName>();

            Stack<Assembly> toLookUpAssemblies = new Stack<Assembly>();
            toLookUpAssemblies.Push(assembly);

            while (toLookUpAssemblies.TryPop(out Assembly result))
            {
                foreach (AssemblyName referencedName in result.GetReferencedAssemblies().OrderByDescending(e => e.Version))
                {
                    try
                    {
                        Assembly loadedAssembly = Assembly.ReflectionOnlyLoad(referencedName.FullName);
                        _referencedAssemblies.Add(referencedName);
                        toLookUpAssemblies.Push(loadedAssembly);
                    }
                    catch (Exception)
                    {
                        _errorAssemblies.Add(referencedName);
                    }
                }
            }

            references = _referencedAssemblies;
            errors = _errorAssemblies;

            return _errorAssemblies.Count == 0;
        }

        /// <summary>
        /// Get all <see cref="Type"/>s from <paramref name="assembly"/> that doesn't produce error from <see cref="ReflectionTypeLoadException"/>.
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> where <see cref="Type"/>s are get.</param>
        /// <returns><see cref="Type"/>s that could be loaded.</returns>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        /// <summary>
        /// Try get all <see cref="Type"/>s from <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> where <see cref="Type"/>s are get.</param>
        /// <param name="loadedTypes"><see cref="Type"/>s that could be loaded.</param>
        /// <param name="exceptions"><see cref="Exception"/>s raised while getting types.</param>
        /// <returns>Whenever it could get all types without error or there were errors.</returns>
        public static bool TryGetTypes(this Assembly assembly, out IEnumerable<Type> loadedTypes, out Exception[] exceptions)
        {
            try
            {
                loadedTypes = assembly.GetTypes();
                exceptions = Array.Empty<Exception>();
                return true;
            }
            catch (ReflectionTypeLoadException exception)
            {
                loadedTypes = exception.Types.Where(e => e != null);
                exceptions = exception.LoaderExceptions;
                return false;
            }
        }
    }
}
