using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application.Searches
{
    public class LogEntrySearch : PagedSearch
    {
        public int? ActorId { get; set; }
        public string? Actor { get; set; }
        public DateTime? CommitedBefore { get; set; }
        public DateTime? CommitedAfter { get; set; }
        public string? UseCaseName { get; set; }
    }
}
