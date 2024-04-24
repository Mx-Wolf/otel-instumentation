using System.Diagnostics;
using Aweton.Mxw.BackEndApi.Abstraction;

namespace Aweton.Mxw.BackEndApi.Services;

public static class ActivitySourceAccessorExtensions
{
    public static IServiceCollection AddActivitySource(this IServiceCollection services, string name)
    {
        return services.AddSingleton<IActivitySourceAccessor>(new ActivitySourceAccessor(new ActivitySource(name)));
    }
}

