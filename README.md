<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>BusOnTime</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 0;
            padding: 20px;
            background-color: #f4f4f4;
        }
        h1, h2, h3 {
            color: #333;
        }
        pre {
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            padding: 10px;
            overflow: auto;
        }
        code {
            background-color: #f4f4f4;
            padding: 2px 4px;
            border-radius: 3px;
            font-size: 90%;
        }
        a {
            color: #0066cc;
            text-decoration: none;
        }
        a:hover {
            text-decoration: underline;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
            background: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .section {
            margin-bottom: 20px;
        }
        .section h2 {
            border-bottom: 2px solid #0066cc;
            padding-bottom: 5px;
        }
        .section ul {
            list-style-type: disc;
            margin-left: 20px;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>BusOnTime</h1>

        <div class="section">
            <h2>Sobre o Projeto</h2>
            <p><strong>BusOnTime</strong> é uma API para gerenciar informações de transporte público, permitindo monitorar horários e estados de equipamentos e modelos de veículos em tempo real.</p>
        </div>

        <div class="section">
            <h2>Índice</h2>
            <ul>
                <li><a href="#sobre-o-projeto">Sobre o Projeto</a></li>
                <li><a href="#funcionalidades">Funcionalidades</a></li>
                <li><a href="#tecnologias-utilizadas">Tecnologias Utilizadas</a></li>
                <li><a href="#instalacao">Instalação</a></li>
                <li><a href="#uso">Uso</a></li>
                <li><a href="#estrutura-do-projeto">Estrutura do Projeto</a></li>
                <li><a href="#testes">Testes</a></li>
                <li><a href="#contribuicao">Contribuição</a></li>
                <li><a href="#licenca">Licença</a></li>
            </ul>
        </div>

        <div class="section">
            <h2 id="sobre-o-projeto">Sobre o Projeto</h2>
            <p><strong>BusOnTime</strong> é uma API desenvolvida em .NET Core para facilitar a gestão de transporte público. O foco do projeto é disponibilizar dados de equipamentos e modelos de veículos, junto com o histórico de suas posições e estados.</p>
            <p>O objetivo principal é otimizar o acompanhamento em tempo real, oferecendo uma maneira eficiente de acessar dados atualizados.</p>
        </div>

        <div class="section">
            <h2 id="funcionalidades">Funcionalidades</h2>
            <ul>
                <li><strong>CRUD de Equipamentos:</strong> Cadastrar, atualizar, listar e excluir equipamentos.</li>
                <li><strong>CRUD de Modelos de Equipamentos:</strong> Gerenciar modelos de equipamentos.</li>
                <li><strong>Gerenciamento de Estado e Histórico:</strong> Acompanhar as mudanças de estado e a posição histórica de equipamentos.</li>
                <li><strong>Autenticação JWT:</strong> Segurança através de tokens JWT para as operações da API.</li>
            </ul>
        </div>

        <div class="section">
            <h2 id="tecnologias-utilizadas">Tecnologias Utilizadas</h2>
            <ul>
                <li><strong>C#</strong> com <strong>.NET Core</strong></li>
                <li><strong>Entity Framework Core</strong> para o mapeamento objeto-relacional</li>
                <li><strong>SQL Server</strong> como banco de dados</li>
                <li><strong>JWT Authentication</strong> para autenticação</li>
                <li><strong>Automapper</strong> para mapeamento entre entidades e DTOs</li>
                <li><strong>xUnit</strong> para testes unitários</li>
            </ul>
        </div>

        <div class="section">
            <h2 id="instalacao">Instalação</h2>
            <h3>Pré-requisitos</h3>
            <ul>
                <li><a href="https://dotnet.microsoft.com/download">.NET SDK</a> (versão 6.0 ou superior)</li>
                <li><a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads">SQL Server</a></li>
                <li>Um gerenciador de pacotes como o <a href="https://www.nuget.org/">NuGet</a></li>
            </ul>
            <h3>Passos</h3>
            <ol>
                <li>Clone o repositório:
                    <pre><code>git clone https://github.com/seu-usuario/BusOnTime.git
cd BusOnTime</code></pre>
                </li>
                <li>Configure o banco de dados no arquivo <code>appsettings.json</code>, atualizando a string de conexão:
                    <pre><code>"ConnectionStrings": {
    "DefaultConnection": "Server=seu-servidor;Database=BusOnTimeDb;Trusted_Connection=True;"
}</code></pre>
                </li>
                <li>Execute as migrações para criar o banco de dados:
                    <pre><code>dotnet ef database update</code></pre>
                </li>
                <li>Restaure as dependências do projeto:
                    <pre><code>dotnet restore</code></pre>
                </li>
                <li>Execute o projeto:
                    <pre><code>dotnet run</code></pre>
                </li>
            </ol>
        </div>

        <div class="section">
            <h2 id="uso">Uso</h2>
            <p>A API oferece vários endpoints para gerenciar as entidades do sistema. Para interagir com os serviços, utilize um cliente como o <a href="https://www.postman.com/">Postman</a> ou <a href="https://swagger.io/">Swagger</a>.</p>
            <h3>Endpoints Principais</h3>
            <ul>
                <li><code>GET /api/equipments</code>: Retorna todos os equipamentos ativos.</li>
                <li><code>POST /api/equipments</code>: Cria um novo equipamento.</li>
                <li><code>PUT /api/equipments/{id}</code>: Atualiza um equipamento existente.</li>
                <li><code>DELETE /api/equipments/{id}</code>: Remove um equipamento (exclusão lógica).</li>
                <li>Outros endpoints para Modelos de Equipamentos, Estados e Histórico estão documentados no Swagger.</li>
            </ul>
        </div>

        <div class="section">
            <h2 id="estrutura-do-projeto">Estrutura do Projeto</h2>
            <pre><code>BusOnTime/
│
├── src/
│   ├── BusOnTime.Api/               # Projeto Web API
│   ├── BusOnTime.Application/       # Camada de Aplicação (Serviços)
│   ├── BusOnTime.Domain/            # Entidades e Interfaces
│   ├── BusOnTime.Infrastructure/    # Repositórios, contexto de banco de dados
│
├── tests/
│   ├── BusOnTime.Tests/             # Testes de Unidade (xUnit)
│
└── README.md</code></pre>
        </div>

        <div class="section">
            <h2 id="testes">Testes</h2>
            <p>Para executar os testes de unidade do projeto, utilize o comando:</p>
            <pre><code>dotnet test</code></pre>
            <p>O projeto de testes usa xUnit para testar as funcionalidades CRUD dos controladores e serviços, garantindo que todas as operações funcionem conforme o esperado.</p>
        </div>

        <div class="section">
            <h2 id="contribuicao">Contribuição</h2>
            <p>Se você deseja contribuir com o projeto, siga os passos abaixo:</p>
            <ol>
                <li>Faça um fork do projeto.</li>
                <li>Crie uma nova branch para suas alterações: <code>git checkout -b minha-branch</code></li>
                <li>Envie suas alterações: <code>git commit -m 'Minha nova feature'</code></li>
                <li>Faça um push da branch: <code>git push origin minha-branch</code></li>
                <li>Envie um pull request.</li>
            </ol>
        </div>

        <div class="section">
            <h2 id="licenca">Licença</h2>
            <p>Este projeto está licenciado sob a licença MIT.</p>
        </div>
    </div>
</body>
</html>
