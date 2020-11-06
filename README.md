# Invillia

## Instruções
- Usuário cria automáticamente no banco ao iniciar a aplicação. <br>
**Usuário:** admin@admin.com <br>
**Senha:** 123456

## Database

<ol>
<li>Executar o comando para criar a instância do sql
<ol>
<li>docker run --name sqlServer2019 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=myP@ssw0rd' -p 1433:1433     -d mcr.microsoft.com/mssql/server:2019-latest</li>
</ol>
</li>
<li>update-database para atualizar o banco</li>
</ol>

## Tecnologias

- Net Core 3.1
- Migration
- JWT
- AutoMapper
- FluentValidation
- xUnit