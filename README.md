<div align="center">

# Desafio Técnico - Corretora de investimentos em ações

</div>

## Descrição do projeto:
Desenvolva uma web api que permita aos usuários simular compras e vendas de ações acompanhar seu portfólio e verificar o a variação de valores das ações.
 

#### Requisitos:
1. Implemente um sistema de autenticação para permitir que os usuários façam login e acessem a plataforma (JWT).
2. Crie um endpoint que exiba uma lista de ações disponíveis, incluindo símbolo, nome e preço atual (O preço deve ser gerado de forma aleatória). 
3. Permita que os usuários pesquisem ações pelo nome ou símbolo.
4. Os usuários devem ser capazes de comprar ações usando um saldo fictício em sua conta.
5. Deve existir um endpoint para depósito, saque e extrato da conta fictícia
6. Mantenha um registro das transações de compra e venda de ações.
7. Implemente uma api onde os usuários possam ver suas ações atuais, o preço atual e o valor total.


## Tecnologias utilizadas:

- ASP .NET CORE
- SQL SERVER
- Entity Framework
- Autenticação JWT

## Instruções para uso:

- Primeiramente é necessário ter uma instância do SQL Server disponível;
- Configure o arquivo `appsettings.json` que está no projeto `StockBrokarageChallenge.WebApi`  com os seguintes campos:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "sua string de conexão",
  },
  "Jwt": {
    "SecretKey": "Seu secret do Token JWT",
    "Issuer": "Seu emissor do token",
    "Audiencie": "Sua audiencia do token",
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

- Após configurar o `appsettings.json`, e com a instância do banco de dados funcionando, rodar o comando `update database` para  atualizar o banco de dados com as migrations do projeto.

## Endpoints:

### POST `/api/files`:

- This endpoint allows to upload CSV files attached as form-data, the name of file must be `csvFile`;
- Example when csv file is updated succesfully:

```json
{
  "message": "File updated succesfully"
}
```

- Example when the file is empty:

```json
{
  "status": "error",
  "message": "Empty file updated"
}
```

- Example when there is no csv file attached:

```json
{
  "status": "error",
  "message": "It's not a csv file"
}
```

- Example when there is an invalid csv file:

```json
{
  "status": "error",
  "message": "Must have the columns 'name', 'city', 'country' and 'favorite_sport' in csv file"
}
```

### GET `/api/users`:

- This endpoint will retrieve the CSV data uploaded, and it allows to use query parameter `?q=` to search every column of the CSV uploaded.
- Response example with no query parameter:

```json
[
  {
    "_id": "6492ec44a6a7483cc01a901b",
    "name": "John Doe",
    "city": "New York",
    "country": "USA",
    "favorite_sport": "Basketball",
    "__v": 0
  },
  {
    "_id": "6492ec44a6a7483cc01a901f",
    "name": "Tom Brown",
    "city": "Sydney",
    "country": "Australia",
    "favorite_sport": "Running",
    "__v": 0
  },
  {
    "_id": "6492ec44a6a7483cc01a9023",
    "name": "Jane Smith",
    "city": "London",
    "country": "UK",
    "favorite_sport": "Football",
    "__v": 0
  },
  {
    "_id": "6492ec44a6a7483cc01a9025",
    "name": "Mike Johnson",
    "city": "Paris",
    "country": "France",
    "favorite_sport": "Tennis",
    "__v": 0
  },
  {
    "_id": "6492ec44a6a7483cc01a901d",
    "name": "Karen Lee",
    "city": "Tokyo",
    "country": "Japan",
    "favorite_sport": "Swimming",
    "__v": 0
  },
  {
    "_id": "6492ec44a6a7483cc01a9021",
    "name": "Emma Wilson",
    "city": "Berlin",
    "country": "Germany",
    "favorite_sport": "Basketball",
    "__v": 0
  }
]
```

- Response example with query parameter `/api/users/?q=basketball`:

```json
[
  {
    "_id": "6492ec44a6a7483cc01a901b",
    "name": "John Doe",
    "city": "New York",
    "country": "USA",
    "favorite_sport": "Basketball",
    "__v": 0
  },
  {
    "_id": "6492ec44a6a7483cc01a9021",
    "name": "Emma Wilson",
    "city": "Berlin",
    "country": "Germany",
    "favorite_sport": "Basketball",
    "__v": 0
  }
]
```

## Improvments:

- Tests improvments

### Final Considerations:

Any doubts or sugestions, contact me by:

- Linkedin: https://www.linkedin.com/in/guicouto90/
- Email: gui.couto90@yahoo.com.br