using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreCqrs.Api.Core.CommandHandlers;
using NetCoreCqrs.Api.Core.Commands;
using NetCoreCqrs.Api.Core.Domain;
using NetCoreCqrs.Api.Core.Events;
using NetCoreCqrs.Api.Core.EventStore;
using NetCoreCqrs.Api.Core.FakeBus;
using NetCoreCqrs.Api.Core.ReadModel;
using NetCoreCqrs.Api.Core.ReadModel.EventHandlers;

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
            services.AddTransient<IReadModelFacade, ReadModelFacade>();
            services.AddTransient<ICommandHandler<CheckInItemsToInventoryCommand>, CheckInItemsToInventoryCommandHandler>();
            services.AddTransient<ICommandHandler<CreateInventoryItemCommand>, CreateInventoryItemCommandHandler>();
            services.AddTransient<ICommandHandler<DeactivateInventoryItemCommand>, DeactivateInventoryItemCommandHandler>();
            services.AddTransient<ICommandHandler<RemoveItemsFromInventoryCommand>, RemoveItemsFromInventoryCommandHandler>();
            services.AddTransient<ICommandHandler<RenameInventoryItemCommand>, RenameInventoryItemCommandHandler>();
            services.AddTransient<IEventHandler<InventoryItemCreatedEvent>, Core.ReadModel.EventHandlers.ForDetails.InventoryItemCreatedEventHandler>();
            services.AddTransient<IEventHandler<InventoryItemDeactivatedEvent>, Core.ReadModel.EventHandlers.ForDetails.InventoryItemDeactivatedEventHandler>();
            services.AddTransient<IEventHandler<InventoryItemRenamedEvent>, Core.ReadModel.EventHandlers.ForDetails.InventoryItemRenamedEventHandler>();
            services.AddTransient<IEventHandler<ItemsCheckedInToInventoryEvent>, Core.ReadModel.EventHandlers.ForDetails.ItemsCheckedInToInventoryEventHandler>();
            services.AddTransient<IEventHandler<ItemsRemovedFromInventoryEvent>, Core.ReadModel.EventHandlers.ForDetails.ItemsRemovedFromInventoryEventHandler>();
            services.AddTransient<IEventHandler<InventoryItemCreatedEvent>, Core.ReadModel.EventHandlers.ForList.InventoryItemCreatedEventHandler>();
            services.AddTransient<IEventHandler<InventoryItemRenamedEvent>, Core.ReadModel.EventHandlers.ForList.InventoryItemRenamedEventHandler>();
            services.AddTransient<IEventHandler<InventoryItemDeactivatedEvent>, Core.ReadModel.EventHandlers.ForList.InventoryItemDeactivatedEventHandler>();
            services.AddTransient<ICommandSender, FakeBus>((serviceProvider) => new FakeBus(serviceProvider));
            services.AddTransient<IEventPublisher, FakeBus>((serviceProvider) => new FakeBus(serviceProvider));
            services.AddTransient<IRepository<InventoryItem>, Repository<InventoryItem>>();
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
