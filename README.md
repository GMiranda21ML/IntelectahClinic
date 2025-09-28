# 🏥 IntelectahClinic

Sistema de gerenciamento de agendamento médico/exame desenvolvido em ASP.NET Core com frontend em HTML/CSS/JavaScript.

## 📋 Sobre o Projeto

O IntelectahClinic é um sistema completo para gerenciamento de de agendamento médico/exame que permite:

- **Cadastro e autenticação de pacientes**
- **Agendamento de consultas e exames**
- **Controle de unidades de atendimento**
- **Dashboard para acompanhamento**

## 🚀 Tecnologias Utilizadas

### Backend
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server LocalDB** - Banco de dados
- **ASP.NET Identity** - Sistema de autenticação e autorização
- **AutoMapper** - Mapeamento de objetos
- **Swagger/OpenAPI** - Documentação da API
- **QuestPDF** - Geração de relatórios em PDF

### Frontend
- **HTML5/CSS3** - Estrutura e estilização
- **JavaScript** - Interatividade
- **Bootstrap 5** - Framework CSS responsivo

## 🏗️ Arquitetura do Projeto

```
IntelectahClinic/
├── Controllers/          # Controladores da API
├── Models/              # Modelos de dados
│   ├── enums/          # Enumerações
├── DTOs/               # Data Transfer Objects
├── Services/           # Lógica de negócio
├── Repository/         # Contexto do banco de dados
├── Profiles/           # Perfis do AutoMapper
├── Migrations/         # Migrações do banco
└── Front/              # Interface do usuário
    ├── scripts/        # JavaScript
    └── styles/         # CSS
```

## 🔧 Configuração e Instalação

### Pré-requisitos
- .NET 8.0
- SQL Server
- Visual Studio 2022 ou VS Code

### Passos para Instalação

1. **Clone o repositório**
```bash
git clone https://github.com/GMiranda21ML/IntelectahClinic.git
cd IntelectahClinic
```

3. **Configure a string de conexão**
   - Verifique o arquivo `appsettings.json`
   - A configuração padrão usa SQL Server LocalDB

4. **Execute as migrações**
```bash
dotnet Update-Database
```

5. **Execute o projeto**
```bash
dotnet run
```

6. **Acesse a aplicação**
   - API: `https://localhost:5502`
   - Swagger: `https://localhost:5502/swagger`
   - Frontend: Abra `Front/index.html` em um servidor local

## 🌐 Principais Endpoints da API

### Autenticação
- `POST /user/cadastro` - Cadastro de paciente
- `POST /user/login` - Login

### Agendamentos
- `GET /agendamento/disponibilidade` - Consultar horários disponíveis
- `POST /agendamento/agendar` - Criar agendamento
- `GET /agendamento/meus-agendamentos` - Listar agendamentos do paciente
- `PUT /agendamento/reagendar/{id}` - Atualizar agendamento
- `DELETE /agendamento/cancelar/{id}` - Cancelar agendamento

### Especialidades
- `GET /especialidade` - Listar especialidades

### Unidades
- `GET /unidade` - Listar unidades

## 📝 Funcionalidades Principais

### Para Pacientes
- ✅ Cadastro e login seguro
- ✅ Visualização de agendamentos
- ✅ Agendamento de consultas e exames
- ✅ Cancelamento e reagendamento

### Para o Sistema
- ✅ Controle de disponibilidade de horários
- ✅ Gerenciamento de especialidades
- ✅ Controle de unidades de atendimento
- ✅ Relatórios e dashboards
- ✅ API RESTful completa

## 👨‍💻 Autor

**Gabriel Miranda Mucarbel de Lima**
