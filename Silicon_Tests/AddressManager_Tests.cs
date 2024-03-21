using Infrastructure.Data.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Silicon_Tests;
public class AddressManager_Tests
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
    public async Task CreateAddressShould_AddOneAddressIfNotExisting_ThenReturnIt()
    {
        // Arrange
        AddressManager _addressManager = new(_addressRepository);

        await CreateAddressShould_CreateNewAddress_ThenReturnIt();
        var newAddress = new AddressEntity()
        {
            AddressLine1 = "Hjälmshultsgatan 11",
            PostalCode = "25431",
            City = "Stockholm"
        };

        // Act
        var result = await _addressManager.CreateAddressAsync(newAddress);
        var addressList = await _addressRepository.ReadAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(addressList.Count() == 2);
    }

    [Fact]
    public async Task UpdateAddressShould_UpdateIfAddressExists_ThenReturnTheUpdatedEntity()
    {
        // Arrange
        AddressManager _addressManager = new(_addressRepository);

        await CreateAddressShould_CreateNewAddress_ThenReturnIt();
        var existingAddress = await _addressRepository.ReadOneAsync(x => x.AddressLine1 == "Hjälmshultsgatan 11");
        var newAddress = new AddressEntity()
        {
            Id = existingAddress.Id,
            AddressLine1 = "Hjälmshultsgatan 11",
            PostalCode = "25431",
            City = "Stockholm"
        };

        // Act
        var result = await _addressManager.UpdateAddressAsync(newAddress);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.City == "Stockholm");
    }
}
