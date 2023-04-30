# Projeto criado com base nas aulas do Rodrigo Branas junto com o curso do Macorati

# Migrations
Para criar uma migration execute o seguinte comando.
`dotnet ef -s src/RestAPI --project src/Infra.Data migrations add nome_da_migration`

Para aplicar uma migration:
`dotnet ef -s src/RestAPI --project src/Infra.Data migrations update`