using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;

namespace Infrastructure.Repositories;
public class UserCourseSubscriptionRepository : Repo<UserCourseSubscriptionEntity>
{
    private readonly DataContext _dataContext;
    public UserCourseSubscriptionRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}