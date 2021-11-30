[![CI](https://github.com/OuterlimitsTech/olt-dotnet-extensions-configuration-flagsmith/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/OuterlimitsTech/olt-dotnet-extensions-configuration-flagsmith/actions/workflows/build.yml) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=OuterlimitsTech_olt-dotnet-extensions-configuration-flagsmith&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=OuterlimitsTech_olt-dotnet-extensions-configuration-flagsmith) [![Nuget](https://img.shields.io/nuget/v/OLT.Extensions.Configuration.Flagsmith)](https://www.nuget.org/packages/OLT.Extensions.Configuration.Flagsmith)

## .NET Core Configuration Extensions to build Flagsmith as a Configuration Provider

### This library was constructed to load Flagsmith features as configuration provider within .NET Core.

#### CHALLENGE: Flagsmith Feature ID are all lowercase, so I used a double underscore to indicate a uppercase letter. I'm open to a better way. :)

```text

app__settings:jwt__secret = AppSettings:JwtSecret

```

#### Configuration Builder example

```csharp
 builder
    .SetBasePath(basePath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

#if DEBUG
  builder.AddUserSecrets<Program>();
#endif

  var config = builder.Build();

  builder.AddFlagsmith(options =>
  {
      options.ApiUrl = config.GetValue<string>("Flagsmith:ApiUrl");
      options.EnvironmentKey = config.GetValue<string>("Flagsmith:ApiKey");
      options.EnabledOnly = true;
  });

  return builder;

```
