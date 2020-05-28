using PortalReflection.Console.Infraestrutura.Ioc;
using System;

namespace PortalReflection.Console.Infraestrutura
{
    public class ControllerResolver
    {
        private readonly IContainer _container;

        public ControllerResolver(IContainer container) => _container = container;

        public object GetController(string nomeController)
        {
            var typeController = Type.GetType(nomeController);
            return _container.GetInstance(typeController);
        }
    }
}
