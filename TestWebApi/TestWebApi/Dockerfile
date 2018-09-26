FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 7000 22

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY TestWebApi/TestWebApi.csproj TestWebApi/
RUN dotnet restore TestWebApi/TestWebApi.csproj

COPY . .
WORKDIR /src/TestWebApi
RUN dotnet build TestWebApi.csproj -c Debug -o /app

FROM build AS publish
RUN dotnet publish TestWebApi.csproj -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN apt update \
  && apt install -y --no-install-recommends openssh-server unzip \
  && mkdir -p /run/sshd \
  && echo "root:1234" | chpasswd

RUN sed -i 's/#PasswordAuthentication yes/PasswordAuthentication yes/g' /etc/ssh/sshd_config
RUN sed -i 's/#PermitRootLogin prohibit-password/PermitRootLogin yes/g' /etc/ssh/sshd_config

ENV NOTVISIBLE "in users profile"
RUN echo "export VISIBLE=now" >> /etc/profile

RUN mkdir /root/.vs-debugger && chmod 0755 /root/.vs-debugger
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v vs2017u1 -l /root/.vs-debugger/

ENTRYPOINT ["/bin/bash", "-c", "/usr/sbin/sshd && dotnet TestWebApi.dll"]
