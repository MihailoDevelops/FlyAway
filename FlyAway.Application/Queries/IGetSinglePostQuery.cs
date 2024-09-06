using FlyAway.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Application.Queries
{
    public interface IGetSinglePostQuery : IQuery<int, ReadPostDto>
    {
    }
}
