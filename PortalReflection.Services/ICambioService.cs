namespace PortalReflection.Services
{
    public interface ICambioService
    {
        decimal calcular(string moedaOrigem, string moedaDestino, decimal valor);
    }
}
