using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Domain.Entities
{
    public class CommentLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public virtual User User { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
