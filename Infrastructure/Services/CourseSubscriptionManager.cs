using Infrastructure.DTOs;
using Infrastructure.Entities.Course;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;
public class CourseSubscriptionManager(UserCourseSubscriptionRepository userCourseRepository, CourseRepository courseRepository, UserRepository userRepository)
{
    private readonly UserCourseSubscriptionRepository _userCourseRepository = userCourseRepository;
    private readonly UserRepository _userRepository = userRepository;
    private readonly CourseRepository _courseRepository = courseRepository;
    public async Task<List<CourseEntity>> GetUserSavedCourses(string userId)
    {
        try
        {
            var subscriptions = await _userCourseRepository.ReadAllAsync();
            var userSubscriptions = new List<CourseEntity>();

            if (subscriptions != null)
            {
                foreach (var subscription in subscriptions)
                {
                    if (subscription.UserId == userId)
                    {
                        userSubscriptions.Add(subscription.CourseIdNavigation);
                    }
                }
                return userSubscriptions;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
    public async Task<UserCourseSubscriptionEntity> CreateCourseSubscription(CourseSubscriptionModel model)
    {
        try
        {
            if (await _courseRepository.Existing(x => x.Id == model.courseId) && await _userRepository.Existing(x => x.Id == model.userId))
            {
                return await _userCourseRepository.CreateAsync(new UserCourseSubscriptionEntity()
                {
                    CourseId = model.courseId,
                    UserId = model.userId,
                });
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
