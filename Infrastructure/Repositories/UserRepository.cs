using Infrastructure.Data.Context;
using Infrastructure.Models.Identification;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class UserRepository : Repo<ApplicationUser>
{
    private readonly DataContext _dataContext;
    public UserRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public override async Task<IEnumerable<ApplicationUser>> ReadAllAsync()
    {
        try
        {
            return await _dataContext.Users
                .Include(i => i.CourseSubscriptions)!.ThenInclude(i => i.CourseIdNavigation)
                .Include(i => i.Address).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public override async Task<ApplicationUser> ReadOneAsync(Expression<Func<ApplicationUser, bool>> predicate)
    {
        try
        {
            var existingEntity = await _dataContext.Users
                .Include(i => i.CourseSubscriptions)!.ThenInclude(i => i.CourseIdNavigation)
                .Include(i => i.Address).FirstOrDefaultAsync(predicate)!;
            if (existingEntity != null)
                return existingEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
