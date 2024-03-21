namespace Silicon;
using Infrastructure.Data.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class Repository_Tests
{
    private readonly static DataContext _dataContext =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

    private readonly AddressRepository _addressRepository = new(_dataContext);

    [Fact]
    public async Task CreateAddressShould_CreateNewAddress_ThenReturnIt()
    {
        // Arrange
        var newAddress = new AddressEntity()
        {
            AddressLine1 = "Hjälmshultsgatan 11",
            PostalCode = "25431",
            City = "Helsingborg"
        };

        // Act
        var result = await _addressRepository.CreateAsync(newAddress);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.AddressLine1 == "Hjälmshultsgatan 11");
    }

    [Fact]
    public async Task ReadOneAddressShould_ReturAddress()
    {
        // Arrange
        await CreateAddressShould_CreateNewAddress_ThenReturnIt();

        // Act
        var result = await _addressRepository.ReadOneAsync(x => x.AddressLine1 == "Hjälmshultsgatan 11");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.AddressLine1 == "Hjälmshultsgatan 11");

    }

    [Fact]
    public async Task ReadAllAddressesShould_ReturnAllAdresses()
    {
        // Arrange
        await CreateAddressShould_CreateNewAddress_ThenReturnIt();

        // Act
        var result = await _addressRepository.ReadAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() == 1);
    }

    [Fact]
    public async Task UpdateAddressShould_UpdateAddress_ThenReturnIt()
    {
        // Arrange
        await CreateAddressShould_CreateNewAddress_ThenReturnIt();
        var existingAddress = await _addressRepository.ReadOneAsync(x => x.AddressLine1 == "Hjälmshultsgatan 11");
        var newAddress = new AddressEntity()
        {
            Id = existingAddress.Id,
            AddressLine1 = "Hjälmshultsgatan 12",
            PostalCode = "25431",
            City = "Helsingborg"
        };

        // Act
        var result = await _addressRepository.UpdateAsync(x => x.Id == existingAddress.Id, newAddress);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.AddressLine1 == newAddress.AddressLine1);
    }


    [Fact]
    public async Task DeleteAddressShould_DeleteAddress_ThenReturnTrue()
    {
        // Arrange
        await CreateAddressShould_CreateNewAddress_ThenReturnIt();
        var existingAddress = await _addressRepository.ReadOneAsync(x => x.AddressLine1 == "Hjälmshultsgatan 11");

        // Act
        var result = await _addressRepository.DeleteAsync(x => x.Id == existingAddress.Id, existingAddress);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistingAddressShould_CheckIfAddressExists_ThenReturnTrue()
    {
        // Arrange
        await CreateAddressShould_CreateNewAddress_ThenReturnIt();

        // Act
        var result = await _addressRepository.Existing(x => x.AddressLine1 == "Hjälmshultsgatan 11");

        // Assert
        Assert.True(result);
    }
}
