Windows IIS. Local deployment instructions:

Build and publish ASP.NET Web API Project using CLI commands: dotnet build dotnet publish

Build Angular project using CLI command: ng build

Create host folder (call it domain for example)

Inside of host folder create API and APP folders. Example of folder structure:

.../test/ api/ app/

Put host.web.config file inside of test folder then replace into "web.config".

Put api.web.config file inside of api folder then replace into "web.config".
