using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalReflection.Console.Infraestrutura.Ioc
{
    public class ContainerSimples : IContainer
    {
        private readonly Dictionary<Type, Type> _mapTypes = new Dictionary<Type, Type>();

        public object GetInstance(Type tipoOrigem)
        {
            if (_mapTypes.ContainsKey(tipoOrigem))
                return GetInstance(_mapTypes[tipoOrigem]);

            var construtores = tipoOrigem.GetConstructors();
            var contrutorSemParametros = construtores.FirstOrDefault(c => c.GetParameters().Any() == false);

            if (contrutorSemParametros != null)
                return contrutorSemParametros.Invoke(new object[0]);

            // buscar aleatoriamente o primeiro construtor com parametros
            var contrutorAtual = construtores[0];
            var parametrosConstrutor = contrutorAtual.GetParameters();
            var valoresParametros = new object[parametrosConstrutor.Count()];

            for (int i = 0; i < parametrosConstrutor.Count(); i++)
            {
                var tipoParametro = parametrosConstrutor[i].ParameterType;
                valoresParametros[i] = GetInstance(tipoParametro);
            }

            return contrutorAtual.Invoke(valoresParametros);
        }

        public void Registrar(Type tipoOrigem, Type tipoDestino)
        {
            if (_mapTypes.ContainsKey(tipoOrigem))
                throw new InvalidOperationException($"Tipo origem {tipoOrigem.Name} ja registrado");

            VerificarTypeOrigem(tipoOrigem, tipoDestino);
            _mapTypes.Add(tipoOrigem, tipoDestino);
        }

        public void Registrar<TOrigem, TDestino>() where TDestino : TOrigem
        {
            if (_mapTypes.ContainsKey(typeof(TOrigem)))
                throw new InvalidOperationException($"Tipo origem {typeof(TOrigem).Name} ja registrado");

            _mapTypes.Add(typeof(TOrigem), typeof(TDestino));
        }

        private void VerificarTypeOrigem(Type tipoOrigem, Type tipoDestino)
        {
            var typeInterfaceImplementada = false;
            var typeClassExtendsImplementada = false;

            if (tipoOrigem.IsInterface)
                typeInterfaceImplementada = tipoDestino.GetInterfaces().Any(interfaceType => interfaceType == tipoOrigem);
            else
                typeClassExtendsImplementada = tipoDestino.IsSubclassOf(tipoOrigem);

            if (!typeInterfaceImplementada || !typeClassExtendsImplementada)
                throw new InvalidOperationException("O tipo destino nao implementa ou herda o tipo origem");
        }
    }
}
