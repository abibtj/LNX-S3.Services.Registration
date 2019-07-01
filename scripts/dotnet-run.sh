#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/S3.Services.Registration
dotnet run --no-restore