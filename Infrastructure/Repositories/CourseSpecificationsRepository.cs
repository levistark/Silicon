using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;

namespace Infrastructure.Repositories;
public class CourseSpecificationsRepository : Repo<CourseSpecificationEntity>
{
    private readonly DataContext _dataContext;
    public CourseSpecificationsRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}