# 🏍️ MotoRental API

API desenvolvida em **.NET 9** para gerenciamento de locações de motocicletas.  
O projeto utiliza **Docker Compose** para orquestrar os containers da aplicação, banco de dados **PostgreSQL** e **RabbitMQ**.

---

## 🚀 Instruções de execução

### 1️ Pré-requisitos

Certifique-se de ter instalado:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/downloads)

---

### 2️ Clonar o repositório

```bash
git clone https://github.com/seu-usuario/motorental.git
cd motorental
```

---

### 3️ Subir os containers

Na raiz do projeto, execute os comandos abaixo para construir e iniciar os containers:
```
docker compose build --no-cache
docker compose up
```

Isso criará e executará a aplicação MotoRental API, o banco de dados PostgreSQL e o serviço de mensageria RabbitMQ

---

### 4️ Acessar o Swagger

Após o ambiente estar em execução, a documentação da API estará disponível em:
- [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)
