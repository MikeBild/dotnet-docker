#BUILD: docker build -t dotnet .
#RUN: docker run --name demo1 -p 8080:8080 -i -t dotnet
#START: docker start demo1
#TEST: open http://<dockerip>:8080

FROM mono:3.10

# Update SSL certs
RUN     mozroots --import --sync

# Copy source files
ADD		src ./src
WORKDIR	/src

#Install development dependencies
RUN		mono /src/NuGet.exe install Nancy
RUN		cp /src/Nancy.1.1/lib/net40/Nancy.dll /src

RUN		mono /src/NuGet.exe install Nancy.Hosting.Self
RUN		cp /src/Nancy.Hosting.Self.1.1/lib/net40/Nancy.Hosting.Self.dll /src

RUN		mono /src/NuGet.exe install LiteDB
RUN		cp /src/LiteDB.1.0.0/lib/net40/LiteDB.dll /src

RUN		mono /src/NuGet.exe install CronNET
RUN		cp /src/CronNET.1.1.4939.24773/lib/net40/CronNET.dll /src

# Compile web service
RUN     mcs nancy.cs modules/webhooks.cs -reference:Nancy.dll -reference:Nancy.Hosting.Self.dll -reference:LiteDB.dll -reference:CronNET.dll
EXPOSE	8080

# Run web service
CMD		["mono", "/src/nancy.exe"]