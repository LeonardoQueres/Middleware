# Middleware
Criei um middleware simples para saudações. 

O middleware deve dizer "Olá" a cada pessoa cujo nome estará na cadeia de consulta.

Middlewares são utilizados na classe startup.cs dos projeto feitos utilizando .net core.

Neste projeto esta sendo feitas algumas validações ao ser feita uma requisição site.

            ''' INICIA A REQUISIÇÃO COM STATUSCODE 400 '''
            context.Response.StatusCode = 400;
            
            ''' VARIFICA O CAMINHO DA REQUISIÇÃO '''
            if (!context.Request.Path.Equals("/saudacoes", System.StringComparison.Ordinal))
            {
                await context.Response.WriteAsync("Caminho de requisição inválido");
                return;
            }
            
            ''' VERIFICA O METODO UTILIZADO PARA ENVIAR OS DADOS '''
            if (!context.Request.Method.Equals("GET"))
            {
                await context.Response.WriteAsync($"{context.Request.Method} Método não suportado");
                return;
            }
            
            ''' VERIFICA SE A VARIAVEL NOMES ESTA PREENCHIDA '''
            if (!context.Request.Query.Any() || string.IsNullOrEmpty(context.Request.Query["nomes"]))
            {
                await context.Response.WriteAsync("A consulta esta vazia ou inválida");
                return;
            }
            
            ''' CASO PASSE POR TODOS OS TESTES PASSA O STATUSCODE PARA 200 '''
            context.Response.StatusCode = 200;
            
            ''' LÊ A QUERYSTRING E TRANSFORMA EM UM ARRAY. '''
            var nomes = context.Request.Query["nomes"].ToString().Split(",").ToList();
            var sb = new StringBuilder();
            
            ''' FAZ UM LOOP PARA LISTAR OS NOMES E INCLUIR NO INICIO A PALAVRA OLÁ '''
            nomes.ForEach(n => sb.Append($"Olá {n}{Environment.NewLine}"));
            
            ''' RETORNA A LISTA DE NOMES '''
            await context.Response.WriteAsync(sb.ToString());

            return;
