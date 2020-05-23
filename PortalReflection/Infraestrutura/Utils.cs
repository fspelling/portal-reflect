using System;
using System.Linq;

namespace PortalReflection.Console.Infraestrutura
{
    public static class Utils
    {
        public static string ConvertPathAssembly(string path)
        {
            var prefixo = "PortalReflection.Console";
            var pathPontos = path.Replace("/", ".");

            return $"{prefixo}{pathPontos}";
        }

        public static string GetTypeContent(string path)
        {
            var pathExtensao = path.Substring(path.LastIndexOf("."));
            var resultado = string.Empty;

            switch (pathExtensao)
            {
                case ".css" :
                    resultado = "text/css; charset=utf-8";
                    break;
                case ".js":
                    resultado = "application/js; charset=utf-8";
                    break;
                default:
                    throw new NotImplementedException("Tipo de conteúdo não previsto!");
            }

            return resultado;
        }

        public static bool IsArquivo(string path)
        {
            var partsPath = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var partLast = partsPath.Last();

            return partLast.Contains('.');
        }
    }
}
