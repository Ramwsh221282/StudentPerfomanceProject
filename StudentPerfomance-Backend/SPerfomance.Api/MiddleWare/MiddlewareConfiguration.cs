using System.IO.Compression;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;

namespace SPerfomance.Api.MiddleWare;

public static class MiddlewareConfiguration
{
    public static IServiceCollection ConfigureMiddleWare(this IServiceCollection services)
    {
        services = services
            .AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                rateLimiterOptions.AddFixedWindowLimiter(
                    "fixed",
                    options =>
                    {
                        options.Window = TimeSpan.FromSeconds(5);
                        options.PermitLimit = 50;
                        options.QueueLimit = 10;
                        options.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
                    }
                );
            })
            .AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            })
            .Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            })
            .Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });
        return services;
    }
}
