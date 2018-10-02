# Docker DotNet Core Microservices Example

**This is example project fast and dirty. In future will be updates and cleaned.**

Root folder is **/TestWebApi**

### Technologies:
1) Docker
2) Nginx (reverse proxy)
3) 2 x WebApi .net core apps
4) MS SQL DB
5) RabbitMq
6) React Static deployed on nginx

### How to run:
- run docker-compose build
- run docker-compose up
- access http://localhost:9999

### How to Debug in VS:
- In TestWebApi project you can check in **Dockerfile** there is installation for **OpenSSH** on that image with **port 22** **user:root** **pass:1234**
- Go to VS (all code in Dockerfile must be builded in **DEBUG** mode)
- Tools -> Options -> Debugging -> General **UNCHECK** "Require source files to exactly match the original version"
- Tools -> Options -> Cross Platform -> Add -> Host name: localhost; Port: 7220; User name: root; Authentication Type: Password; Password: 1234 (port from port forwarding from docker-compose.yml - "7220:22")
- Debug- > Attach to Process -> Select Connection type: SSH; Connection target root@localhost; Select TestWebApi.dll process
- Check Managed (.NET Core for Unix) 
- Put breakpoint where you want

### Project Diagram:

![](https://lh3.googleusercontent.com/XsCL1J3Vad3imP-Y1vzGO1gJNANC4QCxyhofrvf66UQXqCr-Rla7Xbs-ob0V9ODIr8_jyBD7C5e7OH1MAbkq=w1918-h938-rw)
