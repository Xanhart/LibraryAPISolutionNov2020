using LibraryAPI.Models.Books;
using System.Threading.Tasks;

namespace LibraryAPI
{
    public interface IBookCommands
    {
        Task Remove(int id);
        Task<GetBookDetailsResponse> AddBook(PostBookRequest bookToAdd);
        Task<bool> TryToUpdateGenreAsyncIfItsNotTooMuchToAsk(int id, string newGenre);
    }
}