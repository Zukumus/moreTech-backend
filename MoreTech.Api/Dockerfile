####################################################################################
## Build
####################################################################################
#
# Base dotnet image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

ARG USER_NAME=$USER_NAME
ARG TOKEN=$TOKEN
ARG CONFIG_FILE_PATH=$CONFIG_FILE_PATH

#для упрощения жизни копируем все исходники в образ билда
COPY ./ .

# Restore
RUN dotnet restore --no-cache MoreTech.Api/MoreTech.Api.csproj --configfile $CONFIG_FILE_PATH

## Build
RUN dotnet publish MoreTech.Api/MoreTech.Api.csproj -c Release -o /publish/MoreTech.Api --no-restore

#####################################################################################
### Prepare and execution
#####################################################################################

FROM mcr.microsoft.com/dotnet/aspnet:6.0.3

RUN groupadd -g 1000 -r user && \
    useradd -u 1000 -r user -g user -b "/app"
USER user

WORKDIR /app
COPY --chown=user --from=build /publish/MoreTech.Api .

RUN chmod -s -R /app

ENTRYPOINT ["dotnet", "MoreTech.Api.dll"]