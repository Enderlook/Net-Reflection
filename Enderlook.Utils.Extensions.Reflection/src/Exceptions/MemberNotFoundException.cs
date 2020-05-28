using System;

namespace Enderlook.Utils.Extensions
{
    /// <summary>
    /// Exception that is thrown when a member is not found in an type.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<pendiente>")]
    public class MemberNotFoundException : Exception
    {
        /// <summary>
        /// Represent a member named <paramref name="memberName"/> that was not found in <paramref name="type"/>.
        /// </summary>
        /// <param name="memberName">Name of the missing member.</param>
        /// <param name="type">Type where the member is missing.</param>
        public MemberNotFoundException(string memberName, Type type) : base($"No member named {memberName} was found in {nameof(Type)} {type}.") { }
    }
}
