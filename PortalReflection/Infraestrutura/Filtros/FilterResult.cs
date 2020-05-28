namespace PortalReflection.Console.Infraestrutura.Filtros
{
    public class FilterResult
    {
        public bool IsContinue { get; private set; }

        public FilterResult(bool isContinue) => IsContinue = isContinue;
    }
}
