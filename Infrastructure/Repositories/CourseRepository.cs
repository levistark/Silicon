using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class CourseRepository : Repo<CourseEntity>
{
    private readonly DataContext _dataContext;
    public CourseRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public override async Task<IEnumerable<CourseEntity>> ReadAllAsync()
    {
        try
        {
            return await _dataContext.Courses
                .Include(i => i.Author).ThenInclude(x => x.SocialMedia)
                .Include(i => i.CourseBadges)!.ThenInclude(i => i.Badge)
                .Include(i => i.CourseSteps)
                .Include(i => i.Specifications)
                .Include(i => i.Subscribers)
                .Include(i => i.Category)
                .ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public override async Task<CourseEntity> ReadOneAsync(Expression<Func<CourseEntity, bool>> predicate)
    {
        try
        {
            var existingEntity = await _dataContext.Courses
                .Include(i => i.Author).ThenInclude(x => x.SocialMedia)
                .Include(i => i.CourseBadges)!.ThenInclude(i => i.Badge)
                .Include(i => i.CourseSteps)
                .Include(i => i.Specifications)
                .Include(i => i.Subscribers)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(predicate)!;

            if (existingEntity != null)
                return existingEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}