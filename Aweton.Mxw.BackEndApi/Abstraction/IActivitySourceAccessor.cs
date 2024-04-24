using System.Diagnostics;

namespace Aweton.Mxw.BackEndApi.Abstraction;

public interface IActivitySourceAccessor
{
    ActivitySource ActivitySource { get; }
}

