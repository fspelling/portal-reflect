using System;

namespace PortalReflection.Services.Cambio
{
    public class CambioServiceTest : ICambioService
    {
        private readonly Random _rdm = new Random();

        public decimal calcular(string moedaOrigem, string moedaDestino, decimal valor) => (decimal)_rdm.NextDouble() * valor;
    }
}
