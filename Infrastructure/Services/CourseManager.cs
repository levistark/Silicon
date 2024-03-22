using Infrastructure.Repositories;

namespace Infrastructure.Services;
public class CourseManager
{
    private readonly CourseRepository _courseRepository;

    public CourseManager(UserRepository userRepository, CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
}
