using HashEm.Persistence;
using Microsoft.Extensions.Hosting;

namespace HashEm;

public class HashingService(
    HashDbContext db
    ) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
