using PortalReflection.Console.Infraestrutura;
using System.Collections.Generic;

namespace PortalReflection.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var listaPrefixos = new List<string>() { "http://localhost:5341/" };
            var webApplication = new WebApplication(listaPrefixos);
            webApplication.Iniciar();
        }
    }
}
