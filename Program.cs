using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module5HW1;
using Module5HW1.config;
using Module5HW1.services;
using Module5HW1.services.abstractions;

var configuration = new ConfigurationBuilder().AddJsonFile("config.json").Build();
var serviceCollection = new ServiceCollection();

serviceCollection.AddOptions<ApiOptions>().Bind(configuration.GetSection("Api"));

serviceCollection
    .AddLogging(configure => configure.AddConsole())
    .AddHttpClient()
    .AddTransient<IInnerHttpClientService, InnerHttpClientService>()
    .AddTransient<IUserService, UserService>()
    .AddTransient<IRegistrationService, RegistrationService>()
    .AddTransient<IAuthenticationService, AuthenticationService>()
    .AddTransient<IResourceService, ResourceService>()
    .AddTransient<App>();

var app = serviceCollection.BuildServiceProvider().GetService<App>() !;
await app.Start();