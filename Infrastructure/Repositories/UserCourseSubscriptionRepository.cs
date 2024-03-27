using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            return await _dataContext.UserCourseSubscriptions.Include(x => x.CourseIdNavigation).ThenInclude(x => x.Author).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<UserCourseSubscriptionEntity> ReadOneAsync(string UserId, int CourseId)
    {
        try
        {
            var existingEntity = await _dataContext.UserCourseSubscriptions.Include(x => x.CourseIdNavigation).ThenInclude(x => x.Author).FirstOrDefaultAsync(x => x.UserId == UserId && x.CourseId == CourseId);

            if (existingEntity != null)
                return existingEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(UserCourseSubscriptionEntity entity)
    {
        try
        {
            // Check if the entity is being tracked
            var existingEntity = _dataContext.UserCourseSubscriptions.Find(entity.UserId, entity.CourseId);
            if (existingEntity != null)
            {
                // If the entity is being tracked, remove the tracked entity
                _dataContext.UserCourseSubscriptions.Remove(existingEntity);
            }
            else
            {
                // If the entity is not being tracked, attach and remove the entity
                _dataContext.UserCourseSubscriptions.Attach(entity);
                _dataContext.UserCourseSubscriptions.Remove(entity);
            }

            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return false;
    }

    public virtual async Task<bool> Existing(string userId, int courseId)
    {
        try
        {
            return await _dataContext.UserCourseSubscriptions.AnyAsync(pd => pd.UserId == userId && pd.CourseId == courseId);

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}