using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class RedisOnCallLookup : ILookUpOnCallDevs
    {
        private readonly IDistributedCache _cache;

        public RedisOnCallLookup(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetOnCallDevAsync()
        {

            var emailFromCache = await _cache.GetAsync("oncall");
            string email = null;

            if(emailFromCache != null)
            {
                var decodedString = Encoding.UTF8.GetString(emailFromCache);
                email = decodedString;
            }
            else
            {
                await Task.Delay(new Random().Next(1000, 3000));
                email = $"DevMcDevFace-{DateTime.Now.Millisecond}@derp.com";
                var encodedEmail = Encoding.UTF8.GetBytes(email);
                var option = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(15)
                };
                await _cache.SetAsync("oncall", encodedEmail, option);
            }
            return email;
            //return $"DevMcDevFace-{DateTime.Now.Millisecond}@OMG.com";
        }
    }
}
