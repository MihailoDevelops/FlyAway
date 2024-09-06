using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application
{
    public interface IApplicationActor
    {
        public int Id { get; }
        public string Identity { get; }
        IEnumerable<int> AllowedUseCases { get; }
    }
}
