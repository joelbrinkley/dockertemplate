# dockertemplate
This is a template project showing how to structure and containerize a .net application that has a database, angularjs UI, messaging, and message handlers. This example uses Windows Containers.

# Quick Guide

```
    Execute Powershell
    .\_build.ps1 

    .\_local.ps1
    
     Use Browser
     http://localhost:51000
```


## Build

To only build the images and not start containers, run the _build.ps1 powershell script.

`.\_build.ps1`

This powershell script executes the docker-compose command:

`docker-compose -f .\docker-compose.yml -f .\docker-compose.build.yml build`

## Setup Locally

To setup the application locally use the _local.ps1 powershell script.

`.\_local.ps1 `

This powershell script executes the a docker-compose command:

`docker-compose -f .\docker-compose.yml -f .\docker-compose.build.yml -f .\docker-compose.local.yml up -d `

## Docker Compose

The docker-compose files are broken up into different files

### docker-compose.yml
This file contains all of the common docker-compose configuration.

### docker-compose.build.yml
This file contains all of the docker-compose configuration to build an image, such as the docker file to use, and the context of the build.

### docker-compose.<env>.yml
These files contain all the environment specific configuration, such as what ports to publish, env variables, etc.


## Database
This application shows how to configure SQL Server using a container.

The process will use a container to build a dacpac and generate a deployment script.  This script is then executed to build or upgrade a database.

### intialize_database.ps1
This powershell script is used by the container to determine if it needs to setup a new database or attach to an exists and upgrade the schema using a dacpac.



## Client
The client is an angular 4 application.

The docker file to build the client image uses a custom image that has NodeJs and the angular cli already installed.


> joelvbrinkley/angular-build
https://github.com/joelbrinkley/docker-angular-build 







