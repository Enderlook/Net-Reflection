using System;
using System.Reflection;

namespace Enderlook.Utils.Extensions
{
    /// <summary>
    /// Exception that is thrown when a method does require mandatory parameters.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<pendiente>")]
    public class HasMandatoryParametersException : Exception
    {
        /// <summary>
        /// Represents a methods which requires parameters that aren't optional nor params.
        /// </summary>
        /// <param name="methodInfo">Method info found.</param>
        public HasMandatoryParametersException(MethodInfo methodInfo) : base($"{nameof(MethodInfo)} {methodInfo} from {nameof(Type)} {methodInfo.ReflectedType} has parameters which aren't optional nor has the attribute {nameof(ParamArrayAttribute)}.") { }
    }
}
