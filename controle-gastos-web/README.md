# Sistema de Controle de Gastos Residenciais
 
Projeto Full-Stack desenvolvido como desafio técnico, focado em gerenciamento de transações financeiras e controle de fluxo de caixa para residentes.
 
## 🛠 Tecnologias Utilizadas
 
### Back-end

* **.NET 8 (Web API)**

* **C#**

* **Entity Framework Core** (ORM)

* **SQLite** (Banco de dados relacional para persistência local)
 
### Front-end

* **React**

* **TypeScript**

* **Vite**

* **Axios** (Para consumo da API)
 
---
 
## 🚀 Como Executar o Projeto
 
Para rodar o sistema localmente, siga os passos abaixo em terminais separados:
 
### 1. Back-end

1. Navegue até a pasta `ControleGastosApi`.

2. Restaure as dependências e aplique as migrações:

   ```bash

   dotnet ef database update
 