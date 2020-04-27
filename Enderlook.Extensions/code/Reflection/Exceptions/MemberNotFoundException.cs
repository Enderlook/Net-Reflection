using System;

namespace Enderlook.Extensions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<pendiente>")]
    public class MemberNotFoundException : Exception
    {
        public MemberNotFoundException(string memberName, Type type) : base($"No member named {memberName} was found in {nameof(Type)} {type}.") { }
    }
}
