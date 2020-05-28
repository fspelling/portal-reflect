using PortalReflection.Console.CustomAttributes;
using PortalReflection.Services;

namespace PortalReflection.Console.Controller
{
    public class CambioController : BaseController
    {
        private ICambioService _serviceCambio;
        private ICartaoService _serviceCartao;

        public CambioController(ICambioService serviceCambio, ICartaoService serviceCartao)
        {
            _serviceCambio = serviceCambio;
            _serviceCartao = serviceCartao;
        }

        [ApenasHorarioComercimalFiltro]
        public string MXN() => View(new { Valor = _serviceCambio.calcular("MXN", "BRL", 1) });

        [ApenasHorarioComercimalFiltro]
        public string USD() => View(new { Valor = _serviceCambio.calcular("USD", "BRL", 1) });

        [ApenasHorarioComercimalFiltro]
        public string Calculo(string moedaDestino, decimal valor) => Calculo("BRL", moedaDestino, valor);

        [ApenasHorarioComercimalFiltro]
        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor) =>
            View(new
            {
                MoedaOrigem = moedaOrigem,
                ValorMoedaOrigem = valor,
                MoedaDestino = moedaDestino,
                ValorMoedaDestino = _serviceCambio.calcular(moedaOrigem, moedaDestino, valor),
                CartaoPromocao = _serviceCartao.GetCartaoCreditoDestaque()
            });
    }
}
