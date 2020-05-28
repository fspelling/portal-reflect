using System;

namespace PortalReflection.Console.CustomAttributes
{
    public class ApenasHorarioComercimalFiltroAttribute : FiltroAttribute
    {
        public override bool IsContinue() => DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 16;
    }
}
