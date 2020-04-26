using System;

namespace Enderlook.Extensions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<pendiente>")]
    public class MatchingMemberNotFoundException : Exception
    {
        public MatchingMemberNotFoundException(string memberName, Type type, Type returnType) : base($"No member named {memberName} not found in {nameof(Type)} {type} which return {nameof(Type)} (method without mandatory parameters), getter (property) or value (field) is of type {returnType}.") { }
    }
}
