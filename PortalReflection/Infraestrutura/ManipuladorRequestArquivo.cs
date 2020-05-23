using System.Net;
using System.Reflection;

namespace PortalReflection.Console.Infraestrutura
{
    public class ManipuladorRequestArquivo
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            var nomeResource = Utils.ConvertPathAssembly(path);
            var assembly = Assembly.GetExecutingAssembly();
            var streamResource = assembly.GetManifestResourceStream(nomeResource);

            if (streamResource == null)
            {
                resposta.StatusCode = 404;
                resposta.OutputStream.Close();
            }
            else
                using (streamResource)
                {
                    var bytesResource = new byte[streamResource.Length];
                    streamResource.Read(bytesResource, 0, (int)streamResource.Length);

                    // informando o tipo de resposta e o seu conteudo
                    resposta.ContentType = Utils.GetTypeContent(path);
                    resposta.StatusCode = 200;
                    resposta.ContentLength64 = streamResource.Length;
                    resposta.OutputStream.Write(bytesResource, 0, bytesResource.Length);

                    resposta.OutputStream.Close();
                }
        }
    }
}
