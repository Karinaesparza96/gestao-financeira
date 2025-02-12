# **Feedback 1a Entrega - Avaliação Geral e Recomendações**

## Front End

### Navegação

- Navegação simples.
- O dashboard poderia expor graficos, poderia ter mais informações gerais.

### Design

- Design muito simples.

### Funcionalidade

- Não deveria cair direto no dashboard e sim numa tela login/register
- Nenhuma navegação (rota) funciona.

## Back End

### Arquitetura

- Microsoft.AspNetCore.Authentication.JwtBearer na v9 mas o projeto na v8 do .NET
- Erro de inicialização: “SqliteException: SQLite Error 1: 'table "AspNetRoles" already exists'.”
- JwtSettings não deveria estar na camada Business

### Funcionalidade

- Não testado, não estava implementado no front-end

### Modelagem

- Modelagem adequada, validações, mapeamento, tudo ok.

## Projeto

### Organização

- Não versionar a pasta “.vs”

### Documentação

- Não constou informações de como rodar o projeto front-end

### Instalação

- Precisei usar o “npm i —force” devido o uso de algum componente datado