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

- Asp .Net Core
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
    "Issuer": "Emissor do projeto",
    "Audiencie": "Audiencia do projeto",
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

- Após configurar o `appsettings.json`, e com a instância do banco de dados funcionando, rodar o comando `update-database` para  atualizar o banco de dados com as migrations do projeto.

## Endpoints:

### Customer
<b>`POST /api/Customer`</b>
- Endpoint para criação de um novo cliente com uma conta.
- Exemplo do body para requisição:
```json
{
  "name": "string", // Mínimo 3 caracteres,
  "cpf": "string", // 11 caracteres,
  "password": "string", // Mínimo 6 caracteres,
  "confirmPassword": "string", // Igual ao campo password
}
```

- Exemplo de mensagem quando um é cliente criado com sucesso:
```json
Customer created with account number 3
```

### Login
<b>`POST /api/Login`</b>
- Endpoint de autenticação, que gerará um token JWT.
- Exemplo do body para requisição:
```json
{
  "customerCpf": "string", // 11 caracteres,
  "password": "string", // Mínimo 6 caracteres,
  "confirmPassword": "string", // Igual ao campo password
}
```

- Exemplo json de autenticação com sucesso:
```json
{
  "tokenJwt": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjdXN0b21lcklkIjoiMyIsImFjY291bnRJZCI6IjMiLCJhY2NvdW50TnVtYmVyIjoiMyIsImp0aSI6IjVkNzViYTczLTAyNjktNGU0Zi1hNTc3LTdmNTE1ZjM2OTZmNCIsImV4cCI6MTY5NDkwODYyOCwiaXNzIjoiYWRtaW5fc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UiLCJhdWQiOiJjdXN0b21lcnNfc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UifQ.h2ifnBsFRV9d6QlhivCSX_skQETj3VVpvqBmviGbe_Q",
  "expiration": "2023-09-16T23:57:08.5125508Z"
}
```

- Exemplo mensagem de erro na autenticação:
```json
Cpf/password invalid
```

### Stock
<b>`POST /api/Stock`</b>
- Endpoint para criação de uma nova ação. O preço da ação é gerado randomicamente, e o valor é entre 1.0 e 50.0
- Exemplo do body para requisição:
```json
{
  "name": "string", // Mínimo de 2 caracteres
  "code": "string" // 5 caracteres
}
```

- Exemplo json de autenticação com sucesso:
```json
{
  "id": 3,
  "name": "Teste INC",
  "code": "TSTE4",
  "price": 32.18,
  "history": [
    {
      "id": 6,
      "stockId": 3,
      "actualPrice": 32.18,
      "updatedAt": "2023-09-16T20:03:28.9095011-03:00"
    }
  ]
}
```

<b>`GET /api/stock`</b>
- Enpoint para listar todas as ações disponíveis com o seu histórico de preço.
- A cada listagem, atualizará o preço das ações randomicamente. Simulando oscilações que ocorrem no mercado.
- Exemplo de resposta de uma requisição:
```json
[
  {
    "id": 1,
    "name": "VALE",
    "code": "VALE3",
    "price": 4.41,
    "history": [
      {
        "id": 4,
        "stockId": 1,
        "actualPrice": 45.40999984741211,
        "updatedAt": "2023-09-14T18:08:36.2544002"
      },
      {
        "id": 7,
        "stockId": 1,
        "actualPrice": 4.41,
        "updatedAt": "2023-09-16T20:39:00.6492965-03:00"
      }
    ]
  },
  {
    "id": 2,
    "name": "PETROBRAS",
    "code": "PETR4",
    "price": 10.97,
    "history": [
      {
        "id": 5,
        "stockId": 2,
        "actualPrice": 25.850000381469727,
        "updatedAt": "2023-09-14T18:08:36.3036108"
      },
      {
        "id": 8,
        "stockId": 2,
        "actualPrice": 10.97,
        "updatedAt": "2023-09-16T20:39:00.715551-03:00"
      }
    ]
  },
  {
    "id": 3,
    "name": "Teste INC",
    "code": "TSTE4",
    "price": 15.53,
    "history": [
      {
        "id": 6,
        "stockId": 3,
        "actualPrice": 32.18000030517578,
        "updatedAt": "2023-09-16T20:03:28.9095011"
      },
      {
        "id": 9,
        "stockId": 3,
        "actualPrice": 15.53,
        "updatedAt": "2023-09-16T20:39:00.741229-03:00"
      }
    ]
  }
]
```
<b> `GET /api/Stock/code-or-name?filter=` </b>
- Enpoint para listar uma ação pelo nome ou pelo código com o seu histórico de preço.
- A cada listagem, atualizará o preço das ações randomicamente. Simulando oscilações que ocorrem no mercado.
- Exemplo de resposta de uma requisição a url `/api/Stock/code-or-name?filter=PETR4`:
```json
 {
    "id": 2,
    "name": "PETROBRAS",
    "code": "PETR4",
    "price": 10.97,
    "history": [
      {
        "id": 5,
        "stockId": 2,
        "actualPrice": 25.850000381469727,
        "updatedAt": "2023-09-14T18:08:36.3036108"
      },
      {
        "id": 8,
        "stockId": 2,
        "actualPrice": 10.97,
        "updatedAt": "2023-09-16T20:39:00.715551-03:00"
      }
    ]
  }

```

### Account
- Todos os endpoints de `Account` só aceitará requisições autenticadas.
- Para autenticar deve passar o JWT token no header da requisição no seguinte formato:
```json
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjdXN0b21lcklkIjoiMyIsImFjY291bnRJZCI6IjMiLCJhY2NvdW50TnVtYmVyIjoiMyIsImp0aSI6IjE4ZjkwZjhhLTc3NTUtNDEwNC05ODk0LTE4NGUwMDUyODhmMyIsImV4cCI6MTY5NDkxMTk2OCwiaXNzIjoiYWRtaW5fc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UiLCJhdWQiOiJjdXN0b21lcnNfc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UifQ.3WkPmPHbF7iFtbsghLrysprvxGEwcrIZCHywppu1QrY
```
- Requisições sem autenticação retornará a mensagem:

```json
Status Code: 401; Unauthorized
```

<b> `PUT /api/Account/deposit` </b>
- Endpoint para deposito na conta criada.
- Exemplo de json no body da requisição:
```json
{
  "value": "double" // Valor não pode ser igual ou menor que 0.0
}
```
- Exemplo de resposta deposito efetuado com sucesso:
```json
Deposit succeed
```

<b> `PUT /api/Account/withdraw` </b>
- Endpoint para saque na conta criada.
- Exemplo de json no body da requisição:
```json
{
  "value": "double" // Valor não pode ser igual ou menor que 0.0
}
```
- Exemplo de resposta saque efetuado com sucesso:
```json
Withdraw succeed
```
- Exemplo de resposta quando não há saldo suficiente:
```json
There is no enough balance to withdraw
```

<b> `PUT /api/Account/buy-stock` </b>
- Endpoint para compra de ações disponíveis.
- Exemplo de json no body da requisição:
```json
{
  "quantity": "integer value", // A quantidade deve ser maior que 0.
  "stockCode": "string" // String com 5 caracteres.
}
```
- Exemplo de resposta saque efetuado com sucesso:
```json
Purchase succeed - Quantity: 400, Stock PETR4
```
- Exemplo de resposta quando não há saldo suficiente:
```json
There is no enough balance to buy these stocks
```
- Exemplo de resposta quando não encontra uma ação:
```json
Stock Not Found
```

<b> `PUT /api/Account/sell-stock` </b>
- Endpoint para venda de ações disponíveis em sua carteira.
- Exemplo de json no body da requisição:
```json
{
  "quantity": "integer value", // A quantidade deve ser maior que 0.
  "stockCode": "string" // String com 5 caracteres.
}
```
- Exemplo de resposta saque efetuado com sucesso:
```json
Sold succeed - Quantity: 200, Stock PETR4
```
- Exemplo de resposta quando não há ação selecionada em sua carteira:
```json
You dont have this stock in your wallet
```

- Exemplo de resposta quando não há a quantidade informada na venda, é menor que a quantidade que se tem em carteira de uma determinada ação:
```json
Quantity must be lower than StockQuantity
```

<b>`GET /api/Account/transaction-history`</b>
- Endpoint que lista todas as transações efetuadas pela conta logada.
- Exemplo jsson de resposta:
```json
{
  "id": 5,
  "customerId": 5,
  "customer": null,
  "accountNumber": 5,
  "balance": 1727.0000457763672,
  "transactionHistories": [
    {
      "id": 1014,
      "accountId": 5,
      "typeTransaction": "DEPOSIT",
      "transactionValue": 5000,
      "stockCode": null,
      "stockQuantity": null,
      "stockPrice": null,
      "date": "2023-09-16T22:12:56.9623647"
    },
    {
      "id": 1015,
      "accountId": 5,
      "typeTransaction": "BUY_STOCK",
      "transactionValue": -2313.9999389648438,
      "stockCode": "PETR4",
      "stockQuantity": 200,
      "stockPrice": 11.569999694824219,
      "date": "2023-09-16T22:13:11.8413045"
    },
    {
      "id": 1016,
      "accountId": 5,
      "typeTransaction": "BUY_STOCK",
      "transactionValue": -959.0000152587891,
      "stockCode": "VALE3",
      "stockQuantity": 100,
      "stockPrice": 9.59000015258789,
      "date": "2023-09-16T22:13:21.2386095"
    }
  ]
}
```

<b>`GET /api/Account/stock-transaction-history`</b>
- Endpoint que lista todas as transações de compra e venda de ações efetuadas pela conta logada.
- Exemplo json de resposta:
```json
[
  {
    "id": 1015,
    "accountId": 5,
    "typeTransaction": "BUY_STOCK",
    "transactionValue": -2313.9999389648438,
    "stockCode": "PETR4",
    "stockQuantity": 200,
    "stockPrice": 11.569999694824219,
    "date": "2023-09-16T22:13:11.8413045"
  },
  {
    "id": 1016,
    "accountId": 5,
    "typeTransaction": "BUY_STOCK",
    "transactionValue": -959.0000152587891,
    "stockCode": "VALE3",
    "stockQuantity": 100,
    "stockPrice": 9.59000015258789,
    "date": "2023-09-16T22:13:21.2386095"
  },
  {
    "id": 1017,
    "accountId": 5,
    "typeTransaction": "BUY_STOCK",
    "transactionValue": -220.00000476837158,
    "stockCode": "VALE3",
    "stockQuantity": 100,
    "stockPrice": 2.200000047683716,
    "date": "2023-09-16T22:19:32.2154068"
  },
  {
    "id": 1018,
    "accountId": 5,
    "typeTransaction": "SELL_STOCK",
    "transactionValue": 351.99999809265137,
    "stockCode": "PETR4",
    "stockQuantity": 100,
    "stockPrice": 3.5199999809265137,
    "date": "2023-09-16T22:20:02.6914723"
  },
  {
    "id": 1019,
    "accountId": 5,
    "typeTransaction": "SELL_STOCK",
    "transactionValue": 1585.9999656677246,
    "stockCode": "VALE3",
    "stockQuantity": 100,
    "stockPrice": 15.859999656677246,
    "date": "2023-09-16T22:20:06.5220538"
  }
]
```

<b>`GET /api/Account/wallet`</b>
- Endpoint que lista a carteira de investimento da conta ligada.
- A carteira listará as informações como total investido(`totalInvested`) e saldo atual(`currentBalance`) somando todas as ações disponíveis na carteira.
- A carteira mostrará também os detalhes de cada ação na carteira, e conterá os seguintes campos: o total investido na ação(`totalInvestedStock`), o saldo atual em uma ação(`currentInvestedStock`), o preço médio da ação(`averagePrice`) e a quantidade de ações(`stockQuantity`), e as informações da ação especifica(`stock`).
- Lembrando que, o saldo atual investido em ações oscilará conforme os preços das ações que tem em carteira.
- Exemplo json de resposta:
```json
{
  "totalInvested": 4468.000030517578,
  "currentBalance": 9725,
  "stocksWallet": [
    {
      "id": 4,
      "walletId": 3,
      "stockId": 2,
      "averagePrice": 4.09,
      "stockQuantity": 200,
      "totalInvestedStock": 818.0000305175781,
      "currentInvestedStock": 2314,
      "stock": {
        "id": 2,
        "name": "PETROBRAS",
        "code": "PETR4",
        "price": 11.569999694824219,
        "createdAt": "2023-09-13T19:06:43.9385396",
        "updatedAt": "2023-09-16T21:57:40.7060948",
        "history": []
      }
    },
    {
      "id": 5,
      "walletId": 3,
      "stockId": 1,
      "averagePrice": 4.41,
      "stockQuantity": 400,
      "totalInvestedStock": 1763.9999389648438,
      "currentInvestedStock": 3836,
      "stock": {
        "id": 1,
        "name": "VALE",
        "code": "VALE3",
        "price": 9.59000015258789,
        "createdAt": "2023-09-13T19:06:27.0019085",
        "updatedAt": "2023-09-16T21:57:40.6574131",
        "history": []
      }
    },
    {
      "id": 6,
      "walletId": 3,
      "stockId": 1002,
      "averagePrice": 18.86,
      "stockQuantity": 100,
      "totalInvestedStock": 1886.0000610351562,
      "currentInvestedStock": 3575,
      "stock": {
        "id": 1002,
        "name": "Teste INC",
        "code": "TSTE4",
        "price": 35.75,
        "createdAt": "2023-09-16T20:03:28.9085899",
        "updatedAt": "2023-09-16T21:57:40.7313444",
        "history": []
      }
    }
  ]
}
```