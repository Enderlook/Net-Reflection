using System;
using System.Collections.Generic;
using System.Reflection;

namespace Enderlook.Reflection
{
    public static partial class ReflectionExtensions
    {
        private static T GetInheritedStuff<T>(this Type source, Func<Type, T> Get)
        {
            // https://stackoverflow.com/questions/6961781/reflecting-a-private-field-from-a-base-class
            T info;
            do
                info = Get(source);
            while (info == null && (source = source.BaseType) is Type);
            return info;
        }

        private static IEnumerable<T> GetAllInheritedStuff<T>(this Type source, Func<Type, T> Get)
        {
            // https://stackoverflow.com/questions/6961781/reflecting-a-private-field-from-a-base-class
            do
            {
                T info = Get(source);
                if (info == null)
                    yield break;
                yield return info;
            }
            while ((source = source.BaseType) is Type);
        }

        /// <summary>
        /// Get the field <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Returns the first match.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the field.</param>
        /// <param name="name">Name of the field to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the field.</param>
        /// <returns>The first field which match the name <paramref name="name"/>.</returns>
        public static FieldInfo GetInheritedField(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuff((type) => type.GetField(name, bindingFlags));

        /// <summary>
        /// Get the fields <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Return all the times it's declared.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the field.</param>
        /// <param name="name">Name of the field to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the field.</param>
        /// <returns>All the fields which match the name <paramref name="name"/>.</returns>
        public static IEnumerable<FieldInfo> GetInheritedFields(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetAllInheritedStuff((type) => type.GetField(name, bindingFlags));

        /// <summary>
        /// Get the property <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Returns the first match.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the property.</param>
        /// <param name="name">Name of the property to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the property.</param>
        /// <returns>The first property which match the name <paramref name="name"/>.</returns>
        public static PropertyInfo GetInheritedProperty(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuff((type) => type.GetProperty(name, bindingFlags));

        /// <summary>
        /// Get the property <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Returns the first match.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the property.</param>
        /// <param name="name">Name of the property to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the property.</param>
        /// <returns>The first property which match the name <paramref name="name"/>.</returns>
        public static MemberInfo[] GetInheritedMember(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuff((type) => type.GetMember(name, bindingFlags));

        /// <summary>
        /// Get the methods <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Return all the times it's declared.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the method.</param>
        /// <param name="name">Name of the field to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the method.</param>
        /// <returns>All the methods which match the name <paramref name="name"/>.</returns>
        public static IEnumerable<MemberInfo> GetInheritedMembers(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
        {
            foreach (MemberInfo[] members in source.GetAllInheritedStuff((type) => type.GetMember(name, bindingFlags)))
                foreach (MemberInfo member in members)
                    yield return member;
        }

        /// <summary>
        /// Get the properties <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Return all the times it's declared.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the property.</param>
        /// <param name="name">Name of the field to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the property.</param>
        /// <returns>All the properties which match the name <paramref name="name"/>.</returns>
        public static IEnumerable<PropertyInfo> GetInheritedProperties(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetAllInheritedStuff((type) => type.GetProperty(name, bindingFlags));

        /// <summary>
        /// Get the method <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Returns the first match.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the method.</param>
        /// <param name="name">Name of the method to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the method.</param>
        /// <returns>The first method which match the name <paramref name="name"/>.</returns>
        public static MethodInfo GetInheritedMethod(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuff((type) => type.GetMethod(name, bindingFlags));

        /// <summary>
        /// Get the methods <paramref name="name"/> recursively through the inheritance hierarchy of <paramref name="source"/>.<br/>
        /// Return all the times it's declared.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the method.</param>
        /// <param name="name">Name of the field to get.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the method.</param>
        /// <returns>All the methods which match the name <paramref name="name"/>.</returns>
        public static IEnumerable<MethodInfo> GetInheritedMethods(this Type source, string name, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetAllInheritedStuff((type) => type.GetMethod(name, bindingFlags));

        private static IEnumerable<T> GetInheritedStuffs<T>(this Type source, Func<Type, T[]> Get) where T : MemberInfo
        {
            do
            {
                T[] info = Get(source);
                int length = info.Length;
                for (int i = 0; i < length; i++)
                    yield return info[i];
            }
            while ((source = source.BaseType) is Type);
        }

        /// <summary>
        /// Get all the members recursively through the inheritance hierarchy of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the field.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the field.</param>
        /// <returns>The all the fields recursively through the inheritance hierarchy.</returns>
        public static IEnumerable<MemberInfo> GetInheritedMembers(this Type source, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuffs(e => e.GetMembers(bindingFlags | BindingFlags.DeclaredOnly));

        /// <summary>
        /// Get all the fields recursively through the inheritance hierarchy of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the field.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the field.</param>
        /// <returns>The all the fields recursively through the inheritance hierarchy.</returns>
        public static IEnumerable<FieldInfo> GetInheritedFields(this Type source, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuffs(e => e.GetFields(bindingFlags | BindingFlags.DeclaredOnly));

        /// <summary>
        /// Get all the properties recursively through the inheritance hierarchy of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the properties.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the properties.</param>
        /// <returns>The all the properties recursively through the inheritance hierarchy.</returns>
        public static IEnumerable<PropertyInfo> GetInheritedProperties(this Type source, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuffs(e => e.GetProperties(bindingFlags | BindingFlags.DeclaredOnly));

        /// <summary>
        /// Get all the methods recursively through the inheritance hierarchy of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Initial <see cref="Type"/> used to get the methods.</param>
        /// <param name="bindingFlags"><see cref="BindingFlags"/> used to get the methods.</param>
        /// <returns>The all the methods recursively through the inheritance hierarchy.</returns>
        public static IEnumerable<MethodInfo> GetInheritedMethods(this Type source, BindingFlags bindingFlags = BindingFlags.Default)
            => source.GetInheritedStuffs(e => e.GetMethods(bindingFlags | BindingFlags.DeclaredOnly));
    }
}