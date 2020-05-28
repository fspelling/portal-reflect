using PortalReflection.Console.CustomAttributes;
using PortalReflection.Console.Infraestrutura.Binding;

namespace PortalReflection.Console.Infraestrutura.Filtros
{
    public class FilterResolve
    {
        public FilterResult VerificarFiltros(ActionBinderInfo actionBinderInfo)
        {
            var methodInfo = actionBinderInfo.MethodInfo;
            var attributes = methodInfo.GetCustomAttributes(typeof(FiltroAttribute), false);

            foreach (FiltroAttribute attribute in attributes)
                if (!attribute.IsContinue()) return new FilterResult(false);

            return new FilterResult(true);
        }
    }
}
