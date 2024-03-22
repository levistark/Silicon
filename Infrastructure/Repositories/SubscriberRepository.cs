using Infrastructure.Data.Context;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;
public class SubscriberRepository : Repo<SubscriberEntity>
{
    private readonly DataContext _dataContext;
    public SubscriberRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}
