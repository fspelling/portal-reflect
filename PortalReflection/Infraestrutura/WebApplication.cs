using PortalReflection.Console.Infraestrutura.Ioc;
using PortalReflection.Services;
using PortalReflection.Services.Cambio;
using PortalReflection.Services.Cartao;
using System;
using System.Collections.Generic;
using System.Net;

namespace PortalReflection.Console.Infraestrutura
{
    public class WebApplication
    {
        private List<string> _prefixos;
        private readonly IContainer _container = new ContainerSimples();

        public WebApplication(List<string> prefixos) => _prefixos = prefixos ?? throw new ArgumentException(nameof(prefixos));

        public void Iniciar()
        {
            // carregar injecao de dependencia(ioc)
            ConfigurarIoc();

            while (true)
                ManipularRequest();
        }

        private void ManipularRequest()
        {
            // inicializar http com prefixos da lista
            var httpListener = new HttpListener();
            _prefixos.ForEach(pre => httpListener.Prefixes.Add(pre));
            httpListener.Start();

            // buscando a requisicao e a resposta do contexto http
            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            var path = requisicao.Url.PathAndQuery;

            if (Utils.IsArquivo(path))
            {
                var manipulador = new ManipuladorRequestArquivo();
                manipulador.Manipular(resposta, path);
            }
            else
            {
                var manipulador = new ManipuladorRequestController(_container);
                manipulador.Manipular(resposta, path);
            }

            httpListener.Stop();
        }

        private void ConfigurarIoc()
        {
            _container.Registrar<ICambioService, CambioServiceTest>();
            _container.Registrar<ICartaoService, CartaoServiceTest>();
        }
    }
}
