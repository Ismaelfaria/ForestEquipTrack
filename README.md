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
