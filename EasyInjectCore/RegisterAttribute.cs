using System;

namespace Grax32.EasyInjectCore
{
    public class RegisterAttribute : Attribute
    {
        public RegistrationScopeType Scope { get; set; } = RegistrationScopeType.Transient;
        public RegistrationType RegistrationType { get; set; } = RegistrationType.ImplementedInterfaces;
    }
}
