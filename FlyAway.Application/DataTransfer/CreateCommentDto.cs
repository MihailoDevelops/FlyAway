using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application.DataTransfer
{
    public class CreateCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
