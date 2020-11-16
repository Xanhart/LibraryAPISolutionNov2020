using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models.Books
{
    public class GetBooksResponse
    {
        public List<GetBooksResponseItem> Data { get; set; }
    }

    public class GetBooksResponseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
