using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class CachePrimer : BackgroundService
    {
        private readonly ILogger<CachePrimer> _logger;
        private readonly IDistributedCache _cache;

        public CachePrimer(ILogger<CachePrimer> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000);
                _logger.LogInformation("Every Day is a winding road (except that road is The Road, you know that book about lack of agriculture and prions disease)");
                //await Task.Delay(new Random().Next(10 00, 3000));
                var email = $"DevyMcDevFace-{DateTime.Now.Millisecond}@derp.com";
                var encodedEmail = Encoding.UTF8.GetBytes(email);
                var option = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(15)
                };
                await _cache.SetAsync("oncall", encodedEmail, option);
            }
        }
    }
}
