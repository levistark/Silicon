using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;

namespace Infrastructure.Repositories;
public class BadgeRepository : Repo<BadgeEntity>
{
    private readonly DataContext _dataContext;
    public BadgeRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}