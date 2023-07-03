using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class BookMark
    {
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public IReadOnlyList<BookMarkTag> Tags { get; set; }
    }
}
