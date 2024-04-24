using Microsoft.Extensions.Internal;

namespace Aweton.Mxw.BackEndApi.Services
{
  internal class PlatformSystemClock : ISystemClock
  {
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
  }
}