using HashEm.Data;
using LiteDB;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace HashEm;

public class HashingService(
    HashingDbContext db,
    ILogger<HashingService> log,
    IOptions<HashingOptions> options
    ) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var config = options.Value;

        var files = from path in Directory.EnumerateFiles(config.SearchRoot, "*.*", SearchOption.AllDirectories)

                    let file = Path.GetFileName(path)
                    let extension = Path.GetExtension(path).ToLower()
                    let fullPath = Path.GetFullPath(path)
                    let realativePath = ".\\" + fullPath.Substring(config.SearchRoot.Length).Trim('/', '\\')

                    where !path.StartsWith(config.MetaDataBasePath, StringComparison.InvariantCultureIgnoreCase)

                    where !file.Equals("Thumbs.db", StringComparison.InvariantCultureIgnoreCase)
                    where !file.Equals("ehthumbs_vista.db", StringComparison.InvariantCultureIgnoreCase)

                    where !path.StartsWith(Path.Combine(config.SearchRoot, "lightroom"), StringComparison.InvariantCultureIgnoreCase)

                    where !path.StartsWith(Path.Combine(config.SearchRoot, "$RECYCLE.BIN"), StringComparison.InvariantCultureIgnoreCase)
                    where !path.StartsWith(Path.Combine(config.SearchRoot, "System Volume Information"), StringComparison.InvariantCultureIgnoreCase)

                    select new ImageFile
                    {
                        Extension = extension,
                        FileName = fullPath,
                        RealativePath = realativePath,
                    };

        await Parallel.ForEachAsync(files, cancellationToken, async (file, token) =>
        {
            if (token.IsCancellationRequested)
            {
                log.LogWarning("Canceled!");
                return;
            }

            var exists = db.ImageFiles.Find(i => i.RealativePath == file.RealativePath).FirstOrDefault();
            if (exists == null)
            {
                log.LogInformation($"Hash: {{{nameof(file.RealativePath)}}}", file.RealativePath);

                file.PathHash = await HashStringAsync(file.RealativePath);
                file.Hash = await HashFileAsync(Path.Combine(config.SearchRoot, file.RealativePath));

                file.Id = file.PathHash;

                db.ImageFiles.Insert(file);
            }
            else
            {
                log.LogWarning($"Skip: {{{nameof(file.RealativePath)}}}", file.RealativePath);
            }
        });
    }

    public static async ValueTask<Guid> HashFileAsync(string path)
    {
        using var fs = File.OpenRead(path);
        var hash = await MD5.HashDataAsync(fs);
        return new Guid(hash);
    }

    public static async ValueTask<Guid> HashStringAsync(string value)
    {
        using var buffer = new MemoryStream(Encoding.UTF8.GetBytes(value));
        Memory<byte> outBuffer = new byte[16];
        await MD5.HashDataAsync(buffer, outBuffer);
        return new Guid(outBuffer.ToArray());
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
         Task.CompletedTask;
}
