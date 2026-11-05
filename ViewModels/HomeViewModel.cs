using System.Collections.Generic;
using Web_HW___5.Models;

namespace Web_HW___5.ViewModels
{
    public class HomeViewModel
    {
        public List<BookCardViewModel> Books { get; set; } = new List<BookCardViewModel>();
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}
