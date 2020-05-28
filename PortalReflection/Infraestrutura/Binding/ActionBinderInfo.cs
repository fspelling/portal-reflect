using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace PortalReflection.Console.Infraestrutura.Binding
{
    public class ActionBinderInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<ArgumentoNomeValor> ArgumentoValorList { get; private set; }

        public ActionBinderInfo(MethodInfo methodInfo, IEnumerable<ArgumentoNomeValor> argumentoValorList)
        {
            MethodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));

            if (argumentoValorList == null) throw new ArgumentNullException(nameof(argumentoValorList));
            ArgumentoValorList = new ReadOnlyCollection<ArgumentoNomeValor>(argumentoValorList.ToList());
        }

        public object Invoke(object controller)
        {
            var quantidadeParametros = ArgumentoValorList.Count;

            if (quantidadeParametros == 0)
                return MethodInfo.Invoke(controller, new object[0]);

            var parametrosMethodInfo = MethodInfo.GetParameters();
            var parametrosInvoke = new object[quantidadeParametros];

            for (int i = 0; i <= quantidadeParametros; i++)
            {
                var parametroInfo = parametrosMethodInfo[i];
                var argumento = ArgumentoValorList.Single(a => a.Nome == parametroInfo.Name);

                parametrosInvoke[i] = Convert.ChangeType(argumento.Valor, parametroInfo.ParameterType);
            }

            return MethodInfo.Invoke(controller, parametrosMethodInfo);
        }
    }
}