# escape = `
FROM microsoft/windowsservercore as installer
SHELL [ "powershell", "-Command", "$ErrorActionPreference = 'Stop';$ProgressPreference='silentlyContinue';"]

ENV NGINX_VERSION ="1.13.8"

RUN Invoke-WebRequest -OutFile nginx.zip -UseBasicParsing "http://nginx.org/download/nginx-1.13.8.zip"
RUN Expand-Archive nginx.zip -DestinationPath C:\
RUN Rename-Item "C:\nginx-1.13.8" c:\nginx

FROM microsoft/windowsservercore

EXPOSE 80 443
WORKDIR C:\nginx

COPY --from=installer C:\nginx\ .