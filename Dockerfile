# escape=`
FROM hub.nexdev.net/base/rocketor/buildservice AS builder

SHELL ["cmd", "/S", "/C"]

WORKDIR /dev

# Copy files *.sln, *.csproj, packages.config to restore nuget packages
COPY *.sln .
COPY Fanex.BetList.Example.MVC Fanex.BetList.Example.MVC\
COPY Fanex.BetList.Example.Data Fanex.BetList.Example.Data\
COPY Fanex.BetList.Example.Extension Fanex.BetList.Example.Extension\
COPY packages packages\


RUN `
	# verify nuget packages
	nuget.exe restore Fanex.BetList.Core.sln `
	# build project
	&& MSBuild.exe "Fanex.BetList.Example.MVC\Fanex.BetList.Example.MVC.csproj" `
    /t:Build;WebPublish /m `
    /p:DebugType=None `
    /p:Configuration=Release `
    /p:WebPublishMethod=FileSystem `
    /p:TransformWebConfigEnabled=False `
    /p:LocalizedBuild=true `
    /p:AutoParameterizationWebConfigConnectionStrings=False `
    /p:MarkWebConfigAssistFilesAsExclude=false `
    /p:publishUrl="C:\Compiled" `
    /consoleloggerparameters:ErrorsOnly

WORKDIR C:\Compiled


FROM hub.nexdev.net/base/dotnet/framework/aspnet:4.7.2-windowsservercore-ltsc2019 AS base

WORKDIR C:\inetpub\wwwroot

COPY --from=builder C:\Compiled .

RUN dir .\bin\

ENTRYPOINT ["C:\\LogMonitor\\LogMonitor.exe", "C:\\ServiceMonitor.exe", "w3svc"]