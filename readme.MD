# Projeto criado com base nas aulas do Rodrigo Branas junto com o curso do Macorati

# Migrations
Para criar uma migration execute o seguinte comando.
`dotnet ef -s src/RestAPI --project src/Infra.Data migrations add nome_da_migration`

Para aplicar uma migration:
`dotnet ef -s src/RestAPI --project src/Infra.Data migrations update`


Projeto - Parte 2
 
Testes

1 - N�o deve aplicar cupom de desconto expirado - Feito, preciso validar se a condicional � chamada dentro do useCase, ou da entidade... acredito que seja dentro da entidade por isso estar com uma cara de regra de negocio
2 - Ao fazer um pedido, a quantidade de um item n�o pode ser negativa - feito
3 - Ao fazer um pedido, o mesmo item n�o pode ser informado mais de uma vez
4 - Nenhuma dimens�o do item pode ser negativa
5 - O peso do item n�o pode ser negativo
6 - Deve calcular o valor do frete com base nas dimens�es (altura, largura e profundidade em cm) e o peso dos produtos (em kg)
7 - Deve retornar o pre�o m�nimo de frete caso ele seja superior ao valor calculado


Considere


O valor m�nimo � de R$10,00
Por enquanto, como n�o temos uma forma de calcular a dist�ncia entre o CEP de origem e destino, ser� de 1000 km (fixo)
Utilize a f�rmula abaixo para calcular o valor do frete

F�rmula de C�lculo do Frete

Valor do Frete = dist�ncia (km) * volume (m3) * (densidade/100)

Exemplos de volume ocupado (cubagem)

Camera: 20cm x 15 cm x 10 cm = 0,003 m3
Guitarra: 100cm x 30cm x 10cm = 0,03 m3
Geladeira: 200cm x 100cm x 50cm = 1 m3

Exemplos de densidade

Camera: 1kg / 0,003 m3 = 333kg/m3
Guitarra: 3kg / 0,03 m3 = 100kg/m3
Geladeira: 40kg / 1 m3 = 40kg/m3

Exemplos

produto: Camera
dist�ncia: 1000 (fixo)
volume: 0,003
densidade: 333
pre�o: R$9,90 (1000 * 0,003 * (333/100))
pre�o m�nimo: R$10,00

produto: Guitarra
dist�ncia: 1000 (fixo)
volume: 0,03
densidade: 100
pre�o: R$30,00 (1000 * 0,03 * (100/100))

produto: Geladeira
dist�ncia: 1000 (fixo)
volume: 1
densidade: 40
pre�o: R$400,00 (1000 * 1 * (40/100))