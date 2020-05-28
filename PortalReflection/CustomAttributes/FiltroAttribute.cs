using System;

namespace PortalReflection.Console.CustomAttributes
{
    public abstract class FiltroAttribute : Attribute
    {
        public abstract bool IsContinue();
    }
}