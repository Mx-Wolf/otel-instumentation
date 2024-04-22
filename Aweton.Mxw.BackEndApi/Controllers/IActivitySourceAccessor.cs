using System.Diagnostics;

namespace Aweton.Mxw.BackEndApi.Controllers;

public interface IActivitySourceAccessor
{
  ActivitySource ActivitySource { get; }
}

