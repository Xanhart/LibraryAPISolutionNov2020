using LibraryAPI.Models.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class HealthMonitoringApiServerStatus : IProvideServerStatusInformation
    {
        public GetStatusResponse GetCurrentStatus()
        {
            // Do whatever you need to call the external service... 
            // We'll fake it!
            return new GetStatusResponse
            {
                Message = "Everything is Good! This is the REAL Production instance.",
                CreatedAt = DateTime.Now
            };
        }
    }
}
