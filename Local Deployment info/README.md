Windows IIS. Local deployment instructions:

1. Build and publish ASP.NET Web API Project using CLI commands:
   dotnet build
   dotnet publish

2. Build Angular project using CLI command:
   ng build

3. Create host folder (call it domain for example)

4. Inside of host folder configure the web.config file:

<?xml version="1.0" encoding="UTF-8"?>
<configuration>
    <system.webServer>
        <aspNetCore>
            <environmentVariables>
                <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Development" />
            </environmentVariables>
        </aspNetCore>
		<rewrite>
  			<rules>
    			<rule name="Angular" stopProcessing="true">
      				<match url=".*" />
      				<conditions logicalGrouping="MatchAll">
        				<add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
        				<add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
						<add input="{URL}" pattern="^/api/" negate="true"  />
      				</conditions>
      				<action type="Rewrite" url="/app/" />
    			</rule>
  			</rules>
		</rewrite>
    </system.webServer>
</configuration>

At the "url=" in "<action type="Rewrite" url="/app/" />" set up the client-app folder (frontend). In my case it is "/app/".

Make sure that API calls starts with the same uri pattern:
This line allows rewriter to ignore API calls.

"<add input="{URL}" pattern="^/api/" negate="true"  />"

So that once you refresh angular page, the requests will be send correctly.

5. Inside of host folder create API and APP folders. Example of folder structure:

.../test/
api/
app/

6. Inside of API project configure the web.config:

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath=".\SPerfomance.Api.Module.exe" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="OutOfProcess" />
    </system.webServer>
  </location>
</configuration>

Make sure that processPath matches .exe file of api.

7. Inside of APP project configure the index.html:

Find line of code with <base href="/app/" />

and set up your APP folder (in my case it is '/app/');
