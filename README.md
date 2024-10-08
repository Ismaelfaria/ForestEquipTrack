<h1 align="center">
  Teste Estágio Backend V3
  <br>
  <a href="http://www.amitmerchant.com/electron-markdownify"><img src="https://github.com/aikodigital/teste-backend-estagio-v3/blob/master/img/aiko.png" alt="Markdownify" width="200"></a>
  <br>
  # ForestEquipTrack
  <br>
</h1>

<h4 align="center">E uma aplicação backend projetada para coletar, armazenar e expor dados operacionais de equipamentos utilizados em operações florestais.. <a href="https://www.supergasbras.com.br" target="_blank">ForestEquipTrack</a>.</h4>

<p align="center">
  <a href="#sobre-o-projeto">Sobre o Projeto</a> •
  <a href="#funcionalidades">Funcionalidades</a> •
  <a href="#tecnologias-utilizadas">Tecnologias Utilizadas</a> •
  <a href="#instalação">Instalação</a> •
   <a href="#uso">Uso</a> •
   <a href="#instalação">Instalação</a> •
   <a href="#estrutura-do-projeto">Estrutura do Projeto</a> •
   <a href="#testes">Testes</a> •
   <a href="#contribuição">Contribuição</a> •
   <a href="#licença">Licença</a>
</p>

## Projeto de Desafio Técnico - Coleta de Dados de Equipamentos

Neste projeto, tive a oportunidade de trabalhar em um desafio técnico bastante interessante, que envolvia o desenvolvimento de uma aplicação backend para a coleta e gerenciamento de dados de equipamentos utilizados em operações florestais. O desafio consistiu em expor, através de uma API, informações sobre o histórico de posições e estados dos equipamentos, como por exemplo, se estavam operando, parados ou em manutenção.

Foi uma experiência enriquecedora que me permitiu aplicar e fortalecer várias habilidades. Ao longo do desenvolvimento, aprofundei meu entendimento sobre relacionamento entre tabelas no banco de dados, bem como reforcei meus conhecimentos em testes unitários, essenciais para garantir a confiabilidade da aplicação. Além disso, o projeto foi estruturado com uma arquitetura organizada em camadas: API, Application, Domain e Infrastructure, seguindo boas práticas de desenvolvimento.

Cada etapa foi uma ótima oportunidade para aprender e aplicar conceitos fundamentais de desenvolvimento backend, além de enfrentar desafios reais que simulam situações encontradas em projetos do mundo real. Sinto que essa experiência contribuiu bastante para o meu crescimento técnico e profissional.

## Sobre o Projeto

**ForestEquipTrack** A aplicação fornece uma API que permite acessar informações detalhadas sobre o histórico de posições (via GPS) e estados operacionais dos equipamentos, que variam entre Operando, Parado ou em Manutenção. Através dessa API, gestores podem monitorar e analisar a eficiência do uso dos equipamentos em tempo real, garantindo uma operação mais eficiente e segura.

## Funcionalidades

- **CRUD de Equipamentos:** Cadastrar, atualizar, listar e excluir equipamentos.
- **CRUD de Modelos de Equipamentos:** Gerenciar modelos de equipamentos.
- **Gerenciamento de Estado e Histórico:** Acompanhar as mudanças de estado e a posição histórica de equipamentos.

## Tecnologias Utilizadas

- **C#** com **.NET Core**
- **Entity Framework Core** para o mapeamento objeto-relacional
- **SQL Server** como banco de dados
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

2. Execute as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update

3. Restaure as dependências do projeto:
   ```bash
   dotnet restore
   
4. Execute o projeto:
   ```bash
   dotnet run
   
5. Configure o banco de dados no arquivo `appsettings.json`, atualizando a string de conexão:
 ```bash
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu-servidor;Database=seu-bancoDeDados;Trusted_Connection=True"
  }
```

6.Uso
A API oferece vários endpoints para gerenciar as entidades do sistema. Para interagir com os serviços, utilize um cliente como o Postman ou Swagger.

**Endpoints Principais**

 **`GET /api/equipments`**
 **`POST /api/equipments`**
 **`PUT /api/equipments/{id}`**
**`DELETE /api/equipments/{id}`**

## Estrutura do Projeto
```bash
ForestEquipTrack/
│
├── src/
│   ├── ForestEquipTrack.Api/            # Projeto Web API
│   ├── ForestEquipTrack.Application/    # Camada de Aplicação (Serviços)
│   ├── ForestEquipTrack.Domain/         # Entidades e Interfaces
│   └── ForestEquipTrack.Infrastructure/ # Repositórios, contexto de banco de dados
│
├── tests/
│   └── ForestEquipTrack.Tests/          # Testes de Unidade (xUnit)
│
└── README.md
```
## Testes
Para executar os testes de unidade do projeto, utilize o comando:

```bash
dotnet test
O projeto de testes usa xUnit para testar as funcionalidades CRUD dos controladores e serviços, garantindo que todas as operações funcionem conforme o esperado.
```
## Contribuição
- Se você deseja contribuir com o projeto, siga os passos abaixo:

- Faça um fork do projeto.

## Crie uma nova branch para suas alterações:

```bash
git checkout -b minha-branch
```
## Envie suas alterações:

```bash
git commit -m 'Minha nova feature'
```
## Faça um push da branch:

```bash
git push origin minha-branch
Envie um pull request.
```
