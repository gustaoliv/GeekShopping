<div align="center" id="top"> 
  <img src="https://github.com/gustaoliv/GeekShopping/blob/main/GeekShopping.Web/wwwroot/images/geek_shopping.png" alt="GeekShopping" width="100px"/>

  &#xa0;
</div>

<h1 align="center">GeekShopping 🛒</h1>


<!-- Status -->

<h4 align="center"> 
	🚀  Concluído  🚀
</h4> 

<hr>

<p align="center">
  <a href="#dart-sobre">Sobre</a> &#xa0; | &#xa0; 
  <a href="#sparkles-funcionalidades">Funcionalidades</a> &#xa0; | &#xa0;
  <a href="#rocket-tecnologias">Tecnologias</a> &#xa0; | &#xa0;
  <a href="#white_check_mark-pré-requisitos">Pré requisitos</a> &#xa0; | &#xa0;
  <a href="#checkered_flag-começando">Começando</a> &#xa0; | &#xa0;
  <a href="https://github.com/gustavoliv" target="_blank">Autor</a>
</p>

<br>

## :dart: Sobre ##

Projeto de uma loja virtual utilizando microserviços e boas práticas do .Net6

<div align="center"> 
  <img src="GenericImages/Print1.png" alt="TrulyExpenses Website" />
  <img src="GenericImages/Print2.png" alt="TrulyExpenses Website" />
  <img src="GenericImages/Print3.png" alt="TrulyExpenses Website" />
  <img src="GenericImages/Print4.png" alt="TrulyExpenses Website" />
  &#xa0;
</div>


## :sparkles: Funcionalidades ##

:heavy_check_mark: Microsserviço de produtos;\
:heavy_check_mark: Aplicação Web; \
:heavy_check_mark: Validação de token JWT; \
:heavy_check_mark: Listagem de produtos; \
:heavy_check_mark: Microsserviço de carrinho de compras; \
:heavy_check_mark: Microsserviço de cupons de desconto; \
:heavy_check_mark: Página de checkout; \
:heavy_check_mark: Integração com RabbitMQ; \
:heavy_check_mark: Microsserviço de tratamento de pedidos; \
:heavy_check_mark: Microsserviço para processamento do pagamento de compras; \
:heavy_check_mark: Microsserviço para envio de emails; 

## :rocket: Tecnologias ##

As seguintes ferramentas foram usadas na construção do projeto:

- [.Net6](https://dotnet.microsoft.com/pt-br/)
- [Duende-IdentityServer](https://duendesoftware.com/products/identityserver)
- [RabbitMQ](https://www.rabbitmq.com/)

## :white_check_mark: Pré requisitos ##

Antes de começar :checkered_flag:, você precisa ter o [Git](https://git-scm.com) instalado em sua maquina.

## :checkered_flag: Começando ##

```bash

# Clone este repositório
$ https://github.com/gustaoliv/GeekShopping.git

# Entre na pasta
$ cd GeekShopping

# Configure as suas variáveis nos appSettings.json

# Inicializar RabbitMQ
$ docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management

# Buildando a solução
$ dotnet build

# Make Migrations
$ dotnet ef migrations update

# Inicializando as aplicações
$ dotnet run GeekShopping.CartAPI
$ dotnet run GeekShopping.Email
$ dotnet run GeekShopping.CouponAPI
$ dotnet run GeekShopping.IdentityServer
$ dotnet run GeekShopping.OrderAPI
$ dotnet run GeekShopping.ProductAPI
$ dotnet run GeekShopping.Web
$ dotnet run GeekShopping.PaymentAPI

```

## :memo: Licença ##

Feito com :heart: por <a href="https://github.com/gustaoliv" target="_blank">Gustavo Oliveira</a>

&#xa0;

<a href="#top">Voltar para o topo</a>
