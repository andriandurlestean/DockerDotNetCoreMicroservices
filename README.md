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

![](https://lh3.googleusercontent.com/4eGw5HM1T9wy8TrOsODJsk-SfdKYds4CWO_zNWPGn8KJk7A5qKbO33kQbCyxO3Frd2mwL6DgJPGUV3Dt19-DQ8dNdENzuZislUbKY1ElhPsnLyu89iJtA8pfD9bYAq1_sr5A4CZhKcAAbX1CkXgISM8RJf_rQIArOIGnVaExtiYRrCJIUcF-Put97V3xJPSrazoDC73cxpwwk_777ELTtSwoFFutHPJJZCjYoFsDEgDXs1Y-cSWxwFLiTUoFOWAOWpTwFoU8riA1eqgrUM2wUYFMcPP42Og0vTBJiUBo2hMEqiqPLeuib-3ORmBb3dWN-EUif4yj9PfDsDIaVPt-gsohY3wb88ZGHS7vms1LouwbppzeUMdxHC28cNa7pakcNJYl96J_S9ABo67KcwWQpS3eKaJNtfOLKqZBcnPCXNYb_kq3OxOJ6suNMC-m7CcbPoLwBpyNUxJ3dNl1DLDwnaZtPd_feWOZZqfWwaDvDUjj-nbmws27VKpEAvUeLSN-D9sbGmWG4J0FC3sQvfFKdh84TPvOSizSYKNCXLdiPH-WJoXF_H9iAov36UAMNFPlbDoVPleqjVfv-1HQ1n0u7JQiRNM0LR9j7RTKKsJJOAJBMuaZMOiezWRC58T8eT6j=w1502-h606-no)
