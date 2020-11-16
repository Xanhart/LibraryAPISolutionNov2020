using System;

namespace LibraryAPI.Models.Books
{
    public class GetBookDetailsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime AddedToInventory { get; set; }
    }
}
