using System.Diagnostics;

namespace Aweton.Mxw.Toolkit;

public class ActivitySourceAccessor(ActivitySource source) : IActivitySourceAccessor
{
  public ActivitySource ActivitySource { get; } = source;
}

