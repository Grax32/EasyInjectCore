using System;

namespace Grax32.EasyInjectCore
{
    [Flags]
    public enum RegistrationType
    {
        Self = 1,
        ImplementedInterfaces = 2
    }
}
