using System.Diagnostics;

namespace Aweton.Mxw.BackEndApi.Controllers;

public static class ActivitySourceAcessorExtentions
{
  public static IServiceCollection AddActivitySource(this IServiceCollection services, string name)
  {
    return services.AddSingleton<IActivitySourceAccessor>(new ActivitySourceAccessor(new ActivitySource(name)));
  }
}

