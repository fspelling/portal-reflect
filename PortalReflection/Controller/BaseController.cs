using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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

        protected string View(object model, [CallerMemberName]string nomeArquivo = null)
        {
            var view = View(nomeArquivo);
            var propriedadesAll = model.GetType().GetProperties();

            var regex = new Regex("\\{{(.*?)\\}}");
            var viewReplace = regex.Replace(view, (match) =>
            {
                var propriedadeFilter = propriedadesAll.Single(prop => prop.Name == match.Groups[1].Value);
                return propriedadeFilter.GetValue(model)?.ToString();
            });

            return viewReplace;
        }
    }
}
