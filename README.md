# NetCoreOptionSample
It's a sample for directly injecting TOption without depending on IOptions in `ASP.NET Core MVC 3.1` project.

## What's the problems of `IOptions<T>` ?

IOptions<TOptions> is the official approach for inject config model with options pattern.

Assumed that we have a option model called `AuthOptions`.
To use IOpstions<T>, You should configure the option model in `Startup.cs`.


**Starup.cs**
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<AuthConfig>(Configuration.GetSection("MyAuth"));
}
```

Then inject `IOptions<AuthConfig>` into constructor.
**HomeController.cs**
```csharp
public class HomeController : Controller
{
    private AuthConfig _authConfig;

    public HomeController(IOptions<AuthConfig> config)
    {
        _authConfig = config.Value;
    }
    //...
}
```

But there is a few cons of IOptions:
- Can not directly inject the config model without depending on IOptions<TOptions>.
- Inappropriate responsibility for the lifecycle of config injection ( By default is singleton ).

To use official approach with IOptions<T>, you can check official link [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1).

## Using Extension method.

To solve the problems above, I wrote the extension methods with combining built-in DI component and `Bind()`.
So that I could directly inject my option model and setup the lifycycle in the `Startup.cs`.

**Starup.cs/ConfigureServices**
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingletonConfig<AuthConfig>(Configuration.GetSection("MyAuth"));
    //services.AddScopedConfig<AuthConfig>(Configuration.GetSection("MyAuth"));
    //services.AddTransientConfig<AuthConfig>(Configuration.GetSection("MyAuth"));

    //services.AddConfig<AuthConfig>(Configuration.GetSection("MyAuth"), ServiceLifetime.Singleton);
    //services.AddConfig<AuthConfig>(Configuration.GetSection("MyAuth"), ServiceLifetime.Scoped);
    //services.AddConfig<AuthConfig>(Configuration.GetSection("MyAuth"), ServiceLifetime.Transient);

    //...
}
```

To learn more implement details, please check `Extensions/ServiceCollectionExtensions.cs` file.

Finally, giving a ⭐️ Giving Star if it did some help for you.

