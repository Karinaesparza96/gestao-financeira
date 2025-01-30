# **[gestao-financeira] - Aplicação de Gestão Financeira com API RESTful e frontend Angular**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **[gestao-financeira]**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Desenvolvimento Full-Stack Avançado com ASP.NET Core**.
O objetivo principal é desenvolver uma aplicação full-stack que permite aos usuários gerenciar suas finanças pessoais. A plataforma oferece funcionalidades como criação de categorias financeiras, registro de transações (entradas e saídas), relatórios dinâmicos e gráficos interativos para análise de dados financeiros.

### **Autores**
- **Karina Esparza**
- **Viliane Oliveira**
- **Andréia Luiza**
- **Jansen Chantal**
- **Vitor**
- **Tiago Bittencourt**
- **Marcelo Santos Menezes**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com o blog.
- **API RESTful:** Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server e Sqlite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Angular
  - HTML/CSS para estilização
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

- src/
  - BlogExpert.Mvc/ - Projeto MVC
  - Api/ - API RESTful
  - Dados/ - Modelos de Dados e Configuração do EF Core
  - Business/ - Serviços de negócios
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **Registro de Usuários:** Permite incluir usuários para utilizar o sistema.
- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários.
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/Karinaesparza96/gestao-financeira.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - Para rodar no modo Development não precisa configurar nada porque um banco de dados do Sqlite é criado automaticamente na primeira execução.
   - Se quiser rodar o projeto no modo Production é preciso configurar a string de conexão no arquivo `appsettings.json` do projeto Api.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

3. **Executar a API:**
   - `cd backend/src/Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5224/swagger/ 
   
5. **Registrar usuário para usar o sistema:**
   - Para usar o sistema é necessário criar um usuário inicial

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5224/swagger/ 

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
