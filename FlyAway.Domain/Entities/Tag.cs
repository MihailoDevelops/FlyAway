using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Domain.Entities
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<PostTag> TagPosts { get; set; } = new HashSet<PostTag>();
    }
}
