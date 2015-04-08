#BUILD: docker build -t dotnet .
#RUN: docker run --name demo1 -p 8080:8080 -i -t dotnet
#START: docker start demo1
#TEST: open http://<dockerip>:8080

FROM mono:3.10

# Update SSL certs
RUN     mozroots --import --sync

# Copy source files
ADD		src ./src

#Install development dependencies
RUN		mono /src/NuGet.exe install Nancy
RUN		mono /src/NuGet.exe install Nancy.Hosting.Self
RUN		cp /src/Nancy.1.1/lib/net40/Nancy.dll /src
RUN		cp /src/Nancy.Hosting.Self.1.1/lib/net40/Nancy.Hosting.Self.dll /src

WORKDIR	/src

# Compile command line app
RUN     mcs /src/helloworld.cs
# Run command line app
CMD     ["mono", "/src/helloworld.exe"]

# Compile web service
RUN     mcs nancy.cs -reference:/src/Nancy.dll -reference:/src/Nancy.Hosting.Self.dll
EXPOSE	8080

# Run web service
CMD		["mono", "/src/nancy.exe"]