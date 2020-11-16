using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models.Status
{
    public class GetStatusResponse
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
