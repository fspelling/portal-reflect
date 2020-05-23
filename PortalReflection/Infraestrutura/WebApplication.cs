using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace PortalReflection.Console.Infraestrutura
{
    public class WebApplication
    {
        private List<string> _prefixos;

        public WebApplication(List<string> prefixos) => _prefixos = prefixos ?? throw new ArgumentException(nameof(prefixos));

        public void Iniciar()
        {
            while(true) ManipularRequest();
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

            var assembly = Assembly.GetExecutingAssembly();
            var path = requisicao.Url.PathAndQuery;

            if (Utils.IsArquivo(path))
            {
                var manipulador = new ManipuladorRequestArquivo();
                manipulador.Manipular(resposta, path);
            }
            else
            {
                var manipulador = new ManipuladorRequestController();
                manipulador.Manipular(resposta, path);
            }

            httpListener.Stop();
        }
    }
}
