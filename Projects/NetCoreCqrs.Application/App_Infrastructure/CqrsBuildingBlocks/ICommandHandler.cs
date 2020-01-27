﻿using System.Threading.Tasks;

namespace NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
