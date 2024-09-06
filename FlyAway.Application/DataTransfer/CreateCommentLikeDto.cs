using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application.DataTransfer
{
    public class CreateCommentLikeDto
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
    }
}
