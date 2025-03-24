# **[gestao-financeira] - Aplicação de Gestão Financeira com API RESTful e frontend Angular**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **[gestao-financeira]**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Desenvolvimento Full-Stack Avançado com ASP.NET Core**.
O objetivo principal é desenvolver uma aplicação full-stack que permite aos usuários gerenciar suas finanças pessoais. A plataforma oferece funcionalidades como criação de categorias financeiras, registro de transações (entradas e saídas), relatórios dinâmicos e gráficos interativos para análise de dados financeiros.

### **Autores**
- **Karina Esparza**
- **Viliane Oliveira**
- **Jansen Chantal**
- **Tiago Bittencourt**
- **Marcelo Menezes**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação Angular:** Interface web para interação com o sistema de gestão financeira.
- **API RESTful:** Exposição dos recursos do sistema de gestão financeira para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso com registro e autenticação de usuários.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagens de Programação:** C#, TypeScript
- **Frameworks:**
  - Node.js
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

- backend/src/
  - Api/ - API RESTful
  - Business/ - Serviços de negócios
  - Data/ - Modelos de Dados e Configuração do EF Core
- frontend/ - Aplicação em Angular
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **Registro de Usuários:** Permite incluir usuários para utilizar o sistema.
- **CRUD para Categorias, Limites e Transações:** Permite criar, editar, visualizar e excluir Categorias, Limites e Transações. As categorias padrões não podem ser editadas ou excluídas.
- **Autenticação e Autorização:** Autenticação e autorização dos usuários registrados.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- Node.js v22.14.0 ou superior
- .NET SDK 8.0 ou superior
- SQL Server (se quiser rodar no modo production)
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
   
4. **Executar o Frontend:**
   - `cd frontend`
   Antes da primeira execução é necessário instalar os pacotes dependentes com o comando abaixo:
   - `npm install`
   Em seguida pode executar o frontend em development:
   - `ng s`
   - Acesse o frontend em: http://localhost:4200/
   
5. **Usuário de teste:**
   - Na carga inicial é criado um usuário para testes. Caso queira utilizá-lo use os seguintes dados:
   - Usuário: teste@teste.com
   - Senha: Teste@123
   
6. **Registrar novo usuário ou usar o usuário de teste para acessar o sistema:**
   - Para acessar o sistema use o usuário de teste ou crie um novo usuário usando a opção Registrar.

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:
http://localhost:5224/swagger/ 

## **9. Aplicação em Angular**

Para executar a aplicação em Angular acesse a seguinte url:
http://localhost:4200/ 

## **10. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
