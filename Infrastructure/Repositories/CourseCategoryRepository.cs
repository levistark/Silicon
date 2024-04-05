using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;
public class CourseCategoryRepository : Repo<CourseCategoryEntity>
{
    private readonly DataContext _dataContext;
    public CourseCategoryRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public override async Task<IEnumerable<CourseCategoryEntity>> ReadAllAsync()
    {
        try
        {
            return await _dataContext.Categories.OrderBy(o => o.Category).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
