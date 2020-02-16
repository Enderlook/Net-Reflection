using System;
using System.Reflection;

namespace Enderlook.Extensions
{
    public static class ReflectionInheritedExtensions
    {
        private static T GetInheritedStuff<T>(this Type source, Func<Type, T> Get) where T : MemberInfo
        {
            // https://stackoverflow.com/questions/6961781/reflecting-a-private-field-from-a-base-class
            T info;
            do
                info = Get(source);
            while (info == null && (source = source.BaseType) is Type);
            return info;
        }

        /// <summary>
        /// Get the field <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the field.</param>
        /// <param name="name">Name of the field to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the field.</param>
        /// <returns>The first field which match the name <paramref name="name"/>.</returns>
        public static FieldInfo GetInheritedField(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default) => source.GetInheritedStuff((type) => type.GetField(name, bindingFlags));

        /// <summary>
        /// Get the property <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the property.</param>
        /// <param name="name">Name of the property to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the property.</param>
        /// <returns>The first property which match the name <paramref name="name"/>.</returns>
        public static PropertyInfo GetInheritedProperty(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default) => source.GetInheritedStuff((type) => type.GetProperty(name, bindingFlags));

        /// <summary>
        /// Get the method <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the method.</param>
        /// <param name="name">Name of the method to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the method.</param>
        /// <returns>The first method which match the name <paramref name="name"/>.</returns>
        public static MethodInfo GetInheritedMethod(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default) => source.GetInheritedStuff((type) => type.GetMethod(name, bindingFlags));
    }
}