# WorkingDaysApp

## Installation

Clone the repository in your local machine

Go to the repository location and run the following commands

```bash
cd WorkingDaysApp.Api
dotnet build -c Release  .\WorkingDaysApp.Api.csproj

cd .\bin\Release\netcoreapp2.1
dotnet WorkingDaysApp.Api.dll
```

## Usage

Access the API on https://localhost:5001/api/workingdays?fromDate=21/8/2019&toDate=21/8/2020
