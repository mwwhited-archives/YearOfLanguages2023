using HashEm.Persistence;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HashEm;

public class HashingService(
    HashDbContext db,
    IOptions<HashingOptions> options
    ) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var config = options.Value;

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
         Task.CompletedTask;
}
