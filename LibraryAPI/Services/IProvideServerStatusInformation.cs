using LibraryAPI.Models.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public interface IProvideServerStatusInformation
    {
        GetStatusResponse GetCurrentStatus();
    }
}
