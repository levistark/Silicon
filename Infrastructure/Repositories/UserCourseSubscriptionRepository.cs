using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class UserCourseSubscriptionRepository : Repo<UserCourseSubscriptionEntity>
{
    private readonly DataContext _dataContext;
    public UserCourseSubscriptionRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
    public override async Task<IEnumerable<UserCourseSubscriptionEntity>> ReadAllAsync()
    {
        try
        {
            return await _dataContext.UserCourseSubscriptions.Include(i => i.CourseIdNavigation).Include(i => i.User).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public override async Task<UserCourseSubscriptionEntity> ReadOneAsync(Expression<Func<UserCourseSubscriptionEntity, bool>> predicate)
    {
        try
        {
            var existingEntity = await _dataContext.UserCourseSubscriptions.Include(i => i.CourseIdNavigation).Include(i => i.User).FirstOrDefaultAsync();

            if (existingEntity != null)
                return existingEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}