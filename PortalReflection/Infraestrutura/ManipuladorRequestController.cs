using PortalReflection.Console.Infraestrutura.Binding;
using System;
using System.Net;
using System.Text;

namespace PortalReflection.Console.Infraestrutura
{
    public class ManipuladorRequestController
    {
        private ActionBinder _actionBinder;

        public ManipuladorRequestController() => _actionBinder = new ActionBinder();

        public void Manipular(HttpListenerResponse resposta, string path)
        {
            // informacoes do nome controller e action
            var partsPath = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partsPath[0];
            var controllerNomeCompleto = $"PortalReflection.Console.Controller.{controllerNome}Controller";

            // intanciando o controller
            var controllerHandler = Activator.CreateInstance("PortalReflection.Console", controllerNomeCompleto);
            var controller = controllerHandler.Unwrap();

            // invocando o metodo da controller
            var actionBindingInfo = _actionBinder.GetActionBinderInfo(controller, path);
            var resultMetodo = (string)actionBindingInfo.Invoke(controller);

            // convertendo o resultado da pagina html em um array de bytes
            var buffer = Encoding.UTF8.GetBytes(resultMetodo);

            // escrever o buffer na resposta do http
            resposta.ContentType = "text/html; charset=utf-8";
            resposta.StatusCode = 200;
            resposta.ContentLength64 = buffer.Length;
            resposta.OutputStream.Write(buffer, 0, buffer.Length);
            resposta.OutputStream.Close();
        }
    }
}
