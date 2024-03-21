using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Silicon_Tests.Course;
public class CourseRepository_Tests
{
    private readonly static DataContext _dataContext =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

    private readonly CourseRepository _courseRepository = new(_dataContext);
    private readonly AuthorRepository _authorRepository = new(_dataContext);

    [Fact]
    public async Task CreateShould_AddNewEntityToDb_ThenReturnIt()
    {
        // Arrange
        var author = await _authorRepository.CreateAsync(new AuthorEntity()
        {
            FirstName = "Levi",
            LastName = "Stark"
        });
        var newCourse = new CourseEntity()
        {
            Title = "Test",
            AuthorId = author.Id,
            Price = "$12.34"
        };

        // Act
        var result = await _courseRepository.CreateAsync(newCourse);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Title == "Test");
        Assert.True(result.Author.FirstName == "Levi");
    }

    [Fact]
    public async Task ReadOneEntityShould_ReturnBadge()
    {
        // Arrange
        await CreateShould_AddNewEntityToDb_ThenReturnIt();

        // Act
        var result = await _courseRepository.ReadOneAsync(x => x.Title == "Best seller");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Title == "Best seller");

    }

    [Fact]
    public async Task ReadAllEntitiesShould_ReturnAllBadges()
    {
        // Arrange
        await CreateShould_AddNewEntityToDb_ThenReturnIt();

        // Act
        var result = await _courseRepository.ReadAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 1);
    }

    [Fact]
    public async Task UpdateEntityShould_UpdateEntity_ThenReturnIt()
    {
        // Arrange
        await CreateShould_AddNewEntityToDb_ThenReturnIt();
        var existinCourse = await _courseRepository.ReadOneAsync(x => x.Title == "Test");
        var newCourse = new CourseEntity()
        {
            Id = existinCourse.Id,
            Title = "Test2",
            AuthorId = existinCourse.AuthorId,
        };

        // Act
        var result = await _courseRepository.UpdateAsync(x => x.Id == existinCourse.Id, newCourse);
        var badgeList = await _courseRepository.ReadAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Title == newCourse.Title);
        Assert.True(badgeList.Count() == 1);
    }


    [Fact]
    public async Task DeleteEntityShould_DeleteEntity_ThenReturnTrue()
    {
        // Arrange
        await CreateShould_AddNewEntityToDb_ThenReturnIt();
        var existinCourse = await _courseRepository.ReadOneAsync(x => x.Title == "Best seller");

        // Act
        var result = await _courseRepository.DeleteAsync(x => x.Id == existinCourse.Id, existinCourse);
        var courseList = await _courseRepository.ReadAllAsync();

        // Assert
        Assert.True(result);
        Assert.True(courseList.Count() == 0);
    }

    [Fact]
    public async Task ExistingEntityShould_CheckIfEntityExists_ThenReturnTrue()
    {
        // Arrange
        await CreateShould_AddNewEntityToDb_ThenReturnIt();

        // Act
        var result = await _courseRepository.Existing(x => x.Title == "Best seller");

        // Assert
        Assert.True(result);
    }
}
