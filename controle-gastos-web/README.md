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
 
## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:
- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (versão LTS recomendada)
- [Git](https://git-scm.com/)

---

## ⚙️ Como rodar o projeto

O projeto é composto por uma API (Back-end) e uma interface Web (Front-end). Para o funcionamento correto, **ambos devem estar rodando simultaneamente**.

### 1. Preparando o Banco de Dados
Na raiz do projeto, navegue até a pasta da API e aplique as migrações:
```bash
cd ControleGastosApi
dotnet ef database update
2. Iniciando o Back-end
Ainda na pasta ControleGastosApi, execute o comando:

Bash
dotnet run
A API estará disponível em: http://localhost:5032

3. Iniciando o Front-end
Abra um novo terminal no VS Code, navegue até a pasta web e execute:

Bash
cd controle-gastos-web
npm install
npm run dev
O projeto estará disponível em: http://localhost:5173

Nota: Mantenha os dois terminais abertos para que a comunicação entre o Front-end e a API funcione perfeitamente.