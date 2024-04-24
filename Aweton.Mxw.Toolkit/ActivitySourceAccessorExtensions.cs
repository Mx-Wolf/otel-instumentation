using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Aweton.Mxw.Toolkit;

public static class ActivitySourceAccessorExtensions
{
    public static IServiceCollection AddActivitySource(this IServiceCollection services, string name)
    {
        return services.AddSingleton<IActivitySourceAccessor>(new ActivitySourceAccessor(new ActivitySource(name)));
    }
}

