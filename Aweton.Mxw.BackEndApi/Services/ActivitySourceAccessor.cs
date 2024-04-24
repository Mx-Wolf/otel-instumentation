using System.Diagnostics;
using Aweton.Mxw.BackEndApi.Abstraction;

namespace Aweton.Mxw.BackEndApi.Services;

internal class ActivitySourceAccessor : IActivitySourceAccessor
{
    private readonly ActivitySource source;
    public ActivitySourceAccessor(ActivitySource source)
    {
        this.source = source;
    }

    public ActivitySource ActivitySource => source;
}

