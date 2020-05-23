using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PortalReflection.Console.Infraestrutura.Binding
{
    public class ActionBinder
    {
        public ActionBinderInfo GetActionBinderInfo(object controller, string path)
        {
            var indiceInterrogacao = path.IndexOf("?");
            var isQueryString = indiceInterrogacao > 0;

            if (!isQueryString)
            {
                var actionNome = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var methodInfo = controller.GetType().GetMethod(actionNome);

                return new ActionBinderInfo(methodInfo, Enumerable.Empty<ArgumentoNomeValor>());
            }
            else
            {
                var controllerActionNome = path.Substring(0, indiceInterrogacao);
                var actionNome = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];

                var queryString = path.Substring(indiceInterrogacao + 1);
                var tuplasNomeValor = GetListArgumentoNomeValor(queryString);
                var parametros = tuplasNomeValor.Select(t => t.Nome).ToArray();

                var methodInfo = GetMethodInfoByActionAndArgumentos(actionNome, parametros, controller);
                return new ActionBinderInfo(methodInfo, tuplasNomeValor);
            }
        }

        private IEnumerable<ArgumentoNomeValor> GetListArgumentoNomeValor(string queryString)
        {
            var tuplasNomeValor = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tupla in tuplasNomeValor)
            {
                var partsTupla = tupla.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                yield return new ArgumentoNomeValor(partsTupla[0], partsTupla[1]);
            }
        }

        private MethodInfo GetMethodInfoByActionAndArgumentos(string nomeAction, string[] argumentos, object controller)
        {
            var argumentosCount = argumentos.Length;
            var bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly;

            var metodos = controller.GetType().GetMethods(bindingFlags);
            var sobrecarga = metodos.Where(m => m.Name == nomeAction);

            foreach (var metodoSobrecarga in sobrecarga)
            {
                var parametros = metodoSobrecarga.GetParameters();
                if (parametros.Count() != argumentosCount) continue;

                var match = parametros.All(p => argumentos.Contains(p.Name));
                if (match) return metodoSobrecarga;
            }

            throw new ArgumentException($"A sobrecarga do metodo {nomeAction} nao encontrado");
        }
    }
}
