using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiddleware.Middleware
{
    public class Saudacoes
    {
        private readonly RequestDelegate _next;

        public Saudacoes(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = 400;

            if (!context.Request.Path.Equals("/saudacoes", System.StringComparison.Ordinal))
            {
                await context.Response.WriteAsync("Caminho de requisição inválido");
                return;
            }

            if (!context.Request.Method.Equals("GET"))
            {
                await context.Response.WriteAsync($"{context.Request.Method} Método não suportado");
                return;
            }

            if (!context.Request.Query.Any() || string.IsNullOrEmpty(context.Request.Query["nomes"]))
            {
                await context.Response.WriteAsync("A consulta esta vazia ou inválida");
                return;
            }

            context.Response.StatusCode = 200;

            var nomes = context.Request.Query["nomes"].ToString().Split(",").ToList();
            var sb = new StringBuilder();

            nomes.ForEach(n => sb.Append($"Olá {n}{Environment.NewLine}"));

            await context.Response.WriteAsync(sb.ToString());

            return;
        }
    }
}
