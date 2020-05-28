using System;

namespace PortalReflection.Console.Infraestrutura.Ioc
{
    public interface IContainer
    {
        void Registrar(Type tipoOrigem, Type tipoDestino);
        void Registrar<TOrigem, TDestino>() where TDestino : TOrigem;
        object GetInstance(Type tipoOrigem);
    }
}
