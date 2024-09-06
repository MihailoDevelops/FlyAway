using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application.Searches
{
    public class CommentSearch : PagedSearch
    {
        public int Id { get; set; }
        public string? Keyword { get; set; }
        public string? Content { get; set; }
        public DateTime? PublishedBefore { get; set; }
        public DateTime? PublishedAfter { get; set; }
    }
}
