using FlyAway.Application.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlyAway.Application
{
    public class UseCaseExecutor
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;

        public UseCaseExecutor(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            if (!_actor.AllowedUseCases.Contains(query.Id))
                throw new UnauthorizedUseCaseException(query, _actor);

            _logger.Add(new UseCaseLogEntry
            {
                Actor = _actor.Identity,
                ActorId = _actor.Id,
                Data = search,
                UseCaseName = query.Name
            });

            return query.Execute(search);
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            if (!_actor.AllowedUseCases.Contains(command.Id))
                throw new UnauthorizedUseCaseException(command, _actor);

            _logger.Add(new UseCaseLogEntry
            {
                Actor = _actor.Identity,
                ActorId = _actor.Id,
                Data = request,
                UseCaseName = command.Name
            });

            command.Execute(request);
        }
    }
}
