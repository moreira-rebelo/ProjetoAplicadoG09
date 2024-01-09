# SmartStay Connect

<h4 align="center"> 
    <img src="./logo.png"/ alt="Logo da aplicação local">
</h4>

<h4 align="center"> 
  :rocket: Em produção pela AWS.
</h4>

<p align="center">
 <a href="#-sobre-o-projeto">Sobre</a> •
 <a href="#-layout">Layout</a> • 
 <a href="#-como-executar-o-projeto">Como executar</a> • 
</p>

## :computer: Sobre o projeto

A API faz a gestão de acessos a um controlador de quartos de hotel. Esse controlador por sua vez irá controlar o acesso aos quartos e a climatização do quarto.
Na criação de uma reserva a API após a inserção dos dados da reserva, esta irá devolver o número da reserva e a password para acesso à aplicação.
Ao aceder na aplicação com os dados previamente fornecidos, a API irá devolver um token e os detalhes da reserva por exemplo o número do quarto.
Também é possível fazer inserções na base de dados através da API que neste caso será utilizada para adicionar registos na tabela de histórico de acessos aos quartos.

---

## :art: Layout

O layout da aplicação está disponível no Figma:

<a href="">
  <img alt="Layout da app" src="https://img.shields.io/badge/Acessar%20Layout%20-Figma-%2304D361">
</a>

---

## :rocket: Como executar o projeto

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.NET SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks), [Docker](https://www.docker.com/).
Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/)

:compass: Como executar a aplicação

bash
# Clone este repositório
$ git clone <>


bash
# Acesse a pasta ProjetoISI
$ cd ProjetoISI


bash
# Instale as dependências do projeto.
$ dotnet restore


bash
# Instale globalmente o Entity Framework
$ dotnet tool install --global dotnet-e


bash
# Para a configuração da Infraestrutura, acesse ISI.Infrastructure
$ cd ISI.Infrastructure


### Ajuste a string de conexão do Banco de Dados dentro de appsettings.json com o nome ConnectionString

bash
# Atualização do Banco de Dados
$ dotnet ef database update --context IsiDbContext


# Inserção de Dados Manualmente

Insira manualmente os seguintes registros no banco de dados:

- Tabela Controller:

  sql
  INSERT INTO public.controller (controller_address, lock_code)
  VALUES (19216815, 'abc123');
  

- Tabela Room:
  sql
  INSERT INTO public.room (room_number_pk, access_code, "ControllerId")
  VALUES ('Room001', '123abc', 19216815);
  

bash
# Configure a API. Vá para a pasta ISI.Api
$ cd ISI.Api


### Altere o appsettings de desenvolvimento e produção, atualizando a ConnectionString com os dados do seu banco de dados.

`bash
# Compilação do Programa
$ dotnet publish -c Release -o out

bash
# Execução da Aplicação. Entre na pasta out e execute a aplicação:
$ ./ISI.Api
````

---