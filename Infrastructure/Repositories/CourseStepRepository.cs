using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;

namespace Infrastructure.Repositories;
public class CourseStepRepository : Repo<CourseStepEntity>
{
    private readonly DataContext _dataContext;
    public CourseStepRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}