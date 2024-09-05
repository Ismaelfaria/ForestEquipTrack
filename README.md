# BusOnTime

**BusOnTime** é uma API para gerenciar informações de transporte público, permitindo monitorar horários e estados de equipamentos e modelos de veículos em tempo real.

## Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Instalação](#instalação)
- [Uso](#uso)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Testes](#testes)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Sobre o Projeto

**BusOnTime** é uma API desenvolvida em .NET Core para facilitar a gestão de transporte público. O foco do projeto é disponibilizar dados de equipamentos e modelos de veículos, junto com o histórico de suas posições e estados.

O objetivo principal é otimizar o acompanhamento em tempo real, oferecendo uma maneira eficiente de acessar dados atualizados.

## Funcionalidades

- **CRUD de Equipamentos:** Cadastrar, atualizar, listar e excluir equipamentos.
- **CRUD de Modelos de Equipamentos:** Gerenciar modelos de equipamentos.
- **Gerenciamento de Estado e Histórico:** Acompanhar as mudanças de estado e a posição histórica de equipamentos.
- **Autenticação JWT:** Segurança através de tokens JWT para as operações da API.

## Tecnologias Utilizadas

- **C#** com **.NET Core**
- **Entity Framework Core** para o mapeamento objeto-relacional
- **SQL Server** como banco de dados
- **JWT Authentication** para autenticação
- **AutoMapper** para mapeamento entre entidades e DTOs
- **xUnit** para testes unitários

## Instalação

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- Um gerenciador de pacotes como o [NuGet](https://www.nuget.org/)

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/BusOnTime.git
   cd BusOnTime
Configure o banco de dados no arquivo appsettings.json, atualizando a string de conexão:

json
Copiar código
"ConnectionStrings": {
  "DefaultConnection": "Server=seu-servidor;Database=BusOnTimeDb;Trusted_Connection=True;"
}
Execute as migrações para criar o banco de dados:

bash
Copiar código
dotnet ef database update
Restaure as dependências do projeto:

bash
Copiar código
dotnet restore
Execute o projeto:

bash
Copiar código
dotnet run
Uso
A API oferece vários endpoints para gerenciar as entidades do sistema. Para interagir com os serviços, utilize um cliente como o Postman ou Swagger.

Endpoints Principais
GET /api/equipments: Retorna todos os equipamentos ativos.
POST /api/equipments: Cria um novo equipamento.
PUT /api/equipments/{id}: Atualiza um equipamento existente.
DELETE /api/equipments/{id}: Remove um equipamento (exclusão lógica).
Outros endpoints para Modelos de Equipamentos, Estados e Histórico estão documentados no Swagger.
Estrutura do Projeto
bash
Copiar código
BusOnTime/
│
├── src/
│   ├── BusOnTime.Api/            # Projeto Web API
│   ├── BusOnTime.Application/    # Camada de Aplicação (Serviços)
│   ├── BusOnTime.Domain/         # Entidades e Interfaces
│   ├── BusOnTime.Infrastructure/ # Repositórios, contexto de banco de dados
│
├── tests/
│   ├── BusOnTime.Tests/          # Testes de Unidade (xUnit)
│
└── README.md
Testes
Para executar os testes de unidade do projeto, utilize o comando:

bash
Copiar código
dotnet test
O projeto de testes usa xUnit para testar as funcionalidades CRUD dos controladores e serviços, garantindo que todas as operações funcionem conforme o esperado.

Contribuição
Se você deseja contribuir com o projeto, siga os passos abaixo:

Faça um fork do projeto.
Crie uma nova branch para suas alterações:
bash
Copiar código
git checkout -b minha-branch
Envie suas alterações:
bash
Copiar código
git commit -m 'Minha nova feature'
Faça um push da branch:
bash
Copiar código
git push origin minha-branch
Envie um pull request.
Licença
