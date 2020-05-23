using PortalReflection.Services;
using PortalReflection.Services.Cambio;

namespace PortalReflection.Console.Controller
{
    public class CambioController : BaseController
    {
        private ICambioService _service;

        public CambioController() => _service = new CambioServiceTest();

        public string MXN()
        {
            var valorCalc = _service.calcular("MXN", "BRL", 1);

            var conteudoTexto = View();
            conteudoTexto = conteudoTexto.Replace("VALOR_REPLACE", valorCalc.ToString());

            return conteudoTexto;
        }

        public string USD()
        {
            var valorCalc = _service.calcular("USD", "BRL", 1);

            var conteudoTexto = View();
            conteudoTexto = conteudoTexto.Replace("VALOR_REPLACE", valorCalc.ToString());

            return conteudoTexto;
        }

        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor)
        {
            var valorCalc = _service.calcular(moedaOrigem, moedaDestino, valor);

            var conteudoTexto = View();
            conteudoTexto = conteudoTexto.Replace("VALOR_MODEA", valor.ToString())
                                         .Replace("MOEDA_ORIGEM", moedaOrigem)
                                         .Replace("MOEDA_DESTINO", moedaDestino)
                                         .Replace("VALOR_REPLACE", valorCalc.ToString());

            return conteudoTexto;
        }

        public string Calculo(string moedaDestino, decimal valor) => Calculo("BRL", moedaDestino, valor);
    }
}
