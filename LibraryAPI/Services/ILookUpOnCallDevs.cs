using System.Threading.Tasks;

namespace LibraryAPI
{
    public interface ILookUpOnCallDevs
    {
        Task<string> GetOnCallDevAsync();
    }
}