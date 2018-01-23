#escape = `
#reference https://github.com/docker/labs/blob/master/windows/sql-server/part-1.md
#reference https://blogs.msdn.microsoft.com/ssdt/2016/08/22/releasing-ssdt-with-visual-studio-15-preview-4-and-introducing-ssdt-msbuild-nuget-package/
#reference https://blogs.msdn.microsoft.com/ssdt/2016/08/22/part-5-use-your-own-build-and-deployment-agent/
FROM ssdt as build

WORKDIR /Database

COPY /src/Database/ .

RUN msbuild Database.sqlproj /P:Configuration=Release

FROM microsoft/mssql-server-windows-express

ENV ACCEPT_EULA="Y" `
    DATA_PATH="C:\data" `
    sa_password="Pword!!!"

VOLUME ${DATA_PATH}

WORKDIR /BuildTools
COPY --from=build /Microsoft.Data.Tools.Msbuild.10.0.61710.120/lib/net46/ .

WORKDIR C:\init

COPY --from=build C:\Database\bin\Release\Database.dacpac .

COPY \docker\Database\initialize_database.ps1 .

CMD powershell ./initialize_database.ps1 -sa_password $env:sa_password, -data_path $env:DATA_PATH -databaseName TemplateDatabase -VERBOSE

ENTRYPOINT [ "powershell" ]

