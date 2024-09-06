using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application.Searches
{
    public class UserUseCaseSearch : PagedSearch
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? UseCaseId { get; set; }
    }
}
