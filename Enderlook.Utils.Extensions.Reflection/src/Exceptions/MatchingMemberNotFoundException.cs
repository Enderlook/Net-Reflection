using System;

namespace Enderlook.Utils.Extensions
{
    /// <summary>
    /// Exception that is thrown when a member in a type doesn't have the property signature.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<pendiente>")]
    public class MatchingMemberNotFoundException : Exception
    {
        /// <summary>
        /// Represent a member named <paramref name="memberName"/> that was found in <paramref name="type"/> but:
        /// <list type="bullet">
        /// <item>If field, its field type wasn't <paramref name="returnType"/>.</item>
        /// <item>If property, its getter was not found, requires parameters or its return type wasn't <paramref name="returnType"/>.</item>
        /// <item>If method, no overload was found which return type was <paramref name="returnType"/> and didn't require mandatory parameters.</item>
        /// </list>
        /// </summary>
        /// <param name="memberName">Name of the member whose signature doesn't match.</param>
        /// <param name="type">Type where the member was gotten.</param>
        /// <param name="returnType">Return type or field type expected.</param>
        public MatchingMemberNotFoundException(string memberName, Type type, Type returnType) : base($"No member named {memberName} not found in {nameof(Type)} {type} which return {nameof(Type)} (method without mandatory parameters), getter (property) or value (field) is of type {returnType}.") { }
    }
}
