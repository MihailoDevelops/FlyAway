using FlyAway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application.DataTransfer
{
    public class ReadCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string PublishedBy { get; set; }
        public DateTime PublishedAt { get; set; }
        public string CommentPostTitle { get; set; }
        public int TotalLikes { get; set; }
    }
}
