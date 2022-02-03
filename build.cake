var target = Argument("target", "Publish");
var buildConfiguration = Argument("Configuration", "Release");

Task("Restore")
  .Does(() => 
    {
        Information("Restoring Solution Packages");   
        DotNetRestore("src/DemoBlazorApp.sln", 
            new DotNetRestoreSettings()
            {
                NoCache = true
            });
    });

Task("Build")
  .IsDependentOn("Restore")
  .Does(() => 
    {
        Information("Building Solution");
        DotNetBuild("src/DemoBlazorApp.sln",
            new DotNetBuildSettings()
            {
                Configuration = buildConfiguration
            });
    });

Task("Publish")
  .IsDependentOn("Build")
  .Does(() => 
  {
        Information("Publishing Project");
        DotNetBuild("src/DemoBlazorApp/DemoBlazorApp.csproj", 
            new DotNetBuildSettings()
            {
                Configuration = buildConfiguration
            });
  });   

  RunTarget(target);