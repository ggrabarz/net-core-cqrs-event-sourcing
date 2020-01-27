using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreCqrs.Application.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.Application.App_Infrastructure.DomainEventDispatch;
using NetCoreCqrs.Application.Commands.Inventory;
using NetCoreCqrs.Domain.BuildingBlocks;
using NetCoreCqrs.Domain.Inventory;
using NetCoreCqrs.Domain.Inventory.Events;
using NetCoreCqrs.Persistence.EventStore;
using NetCoreCqrs.Persistence.Repository;
using NetCoreCqrs.Persistence.RepositoryCache;
using NetCoreCqrs.Persistence.RepositorySnapshotCache;
using NetCoreCqrs.ReadModel;
using NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.ReadModel.Queries.Inventory;
using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System.Collections.Generic;

namespace NetCoreCqrs.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient<IDomainEventHandler<InventoryItemCreatedEvent>, ReadModel.Queries.Inventory.ForDetails.InventoryItemCreatedEventHandler>();
            services.AddTransient<IDomainEventHandler<InventoryItemDeactivatedEvent>, ReadModel.Queries.Inventory.ForDetails.InventoryItemDeactivatedEventHandler>();
            services.AddTransient<IDomainEventHandler<InventoryItemRenamedEvent>, ReadModel.Queries.Inventory.ForDetails.InventoryItemRenamedEventHandler>();
            services.AddTransient<IDomainEventHandler<ItemsCheckedInToInventoryEvent>, ReadModel.Queries.Inventory.ForDetails.ItemsCheckedInToInventoryEventHandler>();
            services.AddTransient<IDomainEventHandler<ItemsRemovedFromInventoryEvent>, ReadModel.Queries.Inventory.ForDetails.ItemsRemovedFromInventoryEventHandler>();
            services.AddTransient<IDomainEventHandler<InventoryItemCreatedEvent>, ReadModel.Queries.Inventory.ForList.InventoryItemCreatedEventHandler>();
            services.AddTransient<IDomainEventHandler<InventoryItemRenamedEvent>, ReadModel.Queries.Inventory.ForList.InventoryItemRenamedEventHandler>();
            services.AddTransient<IDomainEventHandler<InventoryItemDeactivatedEvent>, ReadModel.Queries.Inventory.ForList.InventoryItemDeactivatedEventHandler>();
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<ICommandHandler<CheckInItemsToInventoryCommand>, CheckInItemsToInventoryCommandHandler>();
            services.AddTransient<ICommandHandler<CreateInventoryItemCommand>, CreateInventoryItemCommandHandler>();
            services.AddTransient<ICommandHandler<DeactivateInventoryItemCommand>, DeactivateInventoryItemCommandHandler>();
            services.AddTransient<ICommandHandler<RemoveItemsFromInventoryCommand>, RemoveItemsFromInventoryCommandHandler>();
            services.AddTransient<ICommandHandler<RenameInventoryItemCommand>, RenameInventoryItemCommandHandler>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddTransient<IQueryHandler<GetInventoryItemsQuery, IEnumerable<InventoryItemListDto>>, GetInventoryItemsQueryHandler>();
            services.AddTransient<IQueryHandler<GetItemDetailsQuery, InventoryItemDetailsDto>, GetItemDetailsQueryHandler>();
            services.AddTransient<IReadModelFacade, ReadModelFacade>();
            services.AddTransient<IRepository<InventoryItem>, Repository<InventoryItem>>();
            services.AddSingleton<IRepositoryCache<InventoryItem>, RepositoryCache<InventoryItem>>();
            services.AddSingleton<IRepositorySnapshotCache<InventoryItem>, RepositorySnapshotCache<InventoryItem>>();
            services.AddTransient<IEventStore, EventStore>();
            services.AddSingleton<FakeReadDatabase>();
            services.AddSingleton<FakeWriteDatabase>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
