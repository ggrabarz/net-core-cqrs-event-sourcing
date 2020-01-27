using NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks;
using System;

namespace NetCoreCqrs.ReadModel.Queries.Inventory
{
    public class GetItemDetailsQuery : IQuery
    {
        public Guid Id { get; set; }

        public GetItemDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
}
