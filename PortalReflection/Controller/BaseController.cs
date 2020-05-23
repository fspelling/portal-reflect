using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PortalReflection.Console.Controller
{
    public abstract class BaseController
    {
        protected string View([CallerMemberName]string nomeArquivo = null)
        {
            var controllerName = GetType().Name.Replace("Controller", string.Empty);
            var nomeCompletoResource = $"PortalReflection.Console.View.{controllerName}.{nomeArquivo}.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamResource = assembly.GetManifestResourceStream(nomeCompletoResource);
            var streamReader = new StreamReader(streamResource);
            var conteudoTexto = streamReader.ReadToEnd();

            return conteudoTexto;
        }
    }
}
