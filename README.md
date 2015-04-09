#Docker + DotNet Microservice

A simple demo with __Docker + Nancy + Hosting + LiteDB__

##Get started

* BUILD: docker build -t dotnet .
* RUN: docker run --name demo1 -p 8080:8080 -i -t dotnet
* START: docker start demo1

##Methods (POSTMAN)
 
* GET http://<dockerip>:8080
* POST http://<dockerip>:8080