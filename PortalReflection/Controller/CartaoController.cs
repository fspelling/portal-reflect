using PortalReflection.Services;

namespace PortalReflection.Console.Controller
{
    public class CartaoController : BaseController
    {
        private readonly ICartaoService _cartaoService;

        public CartaoController(ICartaoService cartaoService) => _cartaoService = cartaoService;

        public string Credito() => View(new { CartaoNome = _cartaoService.GetCartaoCreditoDestaque() });

        public string Debito() => View(new { CartaoNome = _cartaoService.GetCartaoDebitoDestaque() });
    }
}
