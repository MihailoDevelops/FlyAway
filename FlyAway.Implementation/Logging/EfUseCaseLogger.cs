using FlyAway.Application;
using FlyAway.DataAccess;
using FlyAway.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Logging
{
    public class EfUseCaseLogger : IUseCaseLogger
    {
        private readonly FlyAwayContext _context;

        public EfUseCaseLogger(FlyAwayContext context)
        {
            _context = context;
        }

        public void Add(UseCaseLogEntry entry)
        {
            _context.LogEntries.Add(new Domain.Entities.LogEntry
            {
                Actor = entry.Actor,
                ActorId = entry.ActorId,
                UseCaseData = JsonConvert.SerializeObject(entry.Data),
                UseCaseName = entry.UseCaseName,
                CommitedAt = DateTime.UtcNow
            });

            _context.SaveChanges();
        }
    }
}
