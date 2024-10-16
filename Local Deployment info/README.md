Windows IIS. Local deployment instructions:

Build and publish ASP.NET Web API Project using CLI commands: dotnet build dotnet publish

<<<<<<< HEAD
Build Angular project using CLI command: ng build

Create host folder (call it domain for example)

Inside of host folder create API and APP folders. Example of folder structure:

.../test/ api/ app/

Put host.web.config file inside of test folder then replace into "web.config".

Put api.web.config file inside of api folder then replace into "web.config".
=======
2. Build Angular project using CLI command:
   ng build
   
4. Create host folder (call it domain for example)

5. Inside of host folder create API and APP folders. Example of folder structure:

.../test/
	api/
	app/

 6. Put host.web.config file inside of test folder then replace into "web.config".
    
 7. Put api.web.config file inside of api folder then replace into "web.config".
    
 8. Put app.web.config file inside of app folder then replace into "web.config".
>>>>>>> c219ad585f2f57a69263fd125417286d7313bca8
