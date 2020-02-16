using System;
using System.Reflection;

namespace Enderlook.Extensions
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<pendiente>")]
    public class HasMandatoryParametersException : Exception
    {
        public HasMandatoryParametersException(MethodInfo methodInfo) : base($"{nameof(MethodInfo)} {methodInfo} from {nameof(Type)} {methodInfo.ReflectedType} has parameters which aren't optional nor has the attribute {nameof(ParamArrayAttribute)}.") { }
    }
}
