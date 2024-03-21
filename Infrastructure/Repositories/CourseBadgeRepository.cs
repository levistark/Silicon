using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;

namespace Infrastructure.Repositories;
public class CourseBadgeRepository : Repo<CourseBadgeEntity>
{
    private readonly DataContext _dataContext;
    public CourseBadgeRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}