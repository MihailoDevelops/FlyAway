using FlyAway.Application.DataTransfer;
using FlyAway.Application.Queries;
using FlyAway.Application.Searches;
using FlyAway.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Queries
{
    public class EfGetLogEntriesQuery : IGetLogEntriesQuery
    {
        private readonly FlyAwayContext _context;

        public EfGetLogEntriesQuery(FlyAwayContext context)
        {
            _context = context;
        }

        public int Id => 28;

        public string Name => "LogEntries search";

        public PageResponse<LogEntryDto> Execute(LogEntrySearch search)
        {
            var query = _context.LogEntries.AsQueryable();

            if (search.ActorId != null)
                query = query.Where(x => x.ActorId == search.ActorId);

            if (search.Actor != null)
                query = query.Where(x => x.Actor.ToLower().Contains(search.Actor.ToLower()));

            if (search.CommitedBefore != null)
                query = query.Where(x => x.CommitedAt < search.CommitedBefore);

            if (search.CommitedAfter != null)
                query = query.Where(x => x.CommitedAt > search.CommitedAfter);

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PageResponse<LogEntryDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new LogEntryDto
                {
                    Id = x.Id,
                    Actor = x.Actor,
                    ActorId = x.ActorId,
                    UseCaseName = x.UseCaseName,
                    UseCaseData = x.UseCaseData
                }).ToList()
            };

            return response;
        }
    }
}
