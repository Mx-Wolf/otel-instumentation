using System.Diagnostics;

namespace Aweton.Mxw.BackEndApi.Controllers;

internal class ActivitySourceAccessor :IActivitySourceAccessor
{
  private readonly ActivitySource source;
  public ActivitySourceAccessor(ActivitySource source)
  {
    this.source = source;
  }

  public ActivitySource ActivitySource => source;
}

