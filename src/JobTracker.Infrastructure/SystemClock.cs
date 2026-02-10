
using JobTracker.Application.Interfaces;

namespace JobTracker.Infrastructure;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}

