using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Silicon_Tests.Course;
public class BadgeRepository_Tests
{
    private readonly static DataContext _dataContext =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

    private readonly BadgeRepository _badgeRepository = new(_dataContext);

    [Fact]
    public async Task CreateBadgeShould_AddNewBadgeToDb_ThenReturnIt()
    {
        // Arrange
        var newBadge = new BadgeEntity()
        {
            Title = "Best seller",
            BackgroundColor = "Green",
        };

        // Act
        var result = await _badgeRepository.CreateAsync(newBadge);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Title == "Best seller");
    }

    [Fact]
    public async Task ReadOneBadgeShould_ReturnBadge()
    {
        // Arrange
        await CreateBadgeShould_AddNewBadgeToDb_ThenReturnIt();

        // Act
        var result = await _badgeRepository.ReadOneAsync(x => x.Title == "Best seller");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Title == "Best seller");

    }

    [Fact]
    public async Task ReadAllBadgesShould_ReturnAllBadges()
    {
        // Arrange
        await CreateBadgeShould_AddNewBadgeToDb_ThenReturnIt();

        // Act
        var result = await _badgeRepository.ReadAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 1);
    }

    [Fact]
    public async Task UpdateBadgeShould_UpdateBadge_ThenReturnIt()
    {
        // Arrange
        await CreateBadgeShould_AddNewBadgeToDb_ThenReturnIt();
        var existingBadge = await _badgeRepository.ReadOneAsync(x => x.Title == "Best seller");
        var newBadge = new BadgeEntity()
        {
            Id = existingBadge.Id,
            Title = "Digital",
            BackgroundColor = "Light"
        };

        // Act
        var result = await _badgeRepository.UpdateAsync(x => x.Id == existingBadge.Id, newBadge);
        var badgeList = await _badgeRepository.ReadAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Title == newBadge.Title);
        Assert.True(badgeList.Count() == 1);
    }


    [Fact]
    public async Task DeleteBadgeShould_DeleteBadge_ThenReturnTrue()
    {
        // Arrange
        await CreateBadgeShould_AddNewBadgeToDb_ThenReturnIt();
        var existingBadge = await _badgeRepository.ReadOneAsync(x => x.Title == "Best seller");

        // Act
        var result = await _badgeRepository.DeleteAsync(x => x.Id == existingBadge.Id, existingBadge);
        var badgeList = await _badgeRepository.ReadAllAsync();

        // Assert
        Assert.True(result);
        Assert.True(badgeList.Count() == 0);
    }

    [Fact]
    public async Task ExistingBadgeShould_CheckIfBadgeExists_ThenReturnTrue()
    {
        // Arrange
        await CreateBadgeShould_AddNewBadgeToDb_ThenReturnIt();

        // Act
        var result = await _badgeRepository.Existing(x => x.Title == "Best seller");

        // Assert
        Assert.True(result);
    }
}
