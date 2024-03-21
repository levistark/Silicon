using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;
public class AddressManager
{
    private readonly AddressRepository _addressRepository;
    public AddressManager(AddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<AddressEntity> CreateAddressAsync(AddressEntity address)
    {
        try
        {
            // Checking if the street name already exists and retrieves its entity from the database if true
            if (await IsExistingAddress(address))
                return address;
            else
                return await _addressRepository.CreateAsync(address);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<AddressEntity> GetAddressByIdAsync(int id)
    {
        try
        {
            return await _addressRepository.ReadOneAsync(x => x.Id == id);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<AddressEntity> UpdateAddressAsync(AddressEntity newAddress)
    {
        try
        {
            return await _addressRepository.UpdateAsync(x => x.Id == newAddress.Id, newAddress);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> IsExistingAddress(AddressEntity address)
    {
        // Checking if the street name already exists and retrieves its entity from the database if true
        if (await _addressRepository.Existing(x => x.AddressLine1 == address.AddressLine1))
        {
            var existingLine1 = await _addressRepository.ReadOneAsync(x => x.AddressLine1 == address.AddressLine1);

            // Checking if the postal code already exists and retrieves its entity from the database if true
            if (await _addressRepository.Existing(x => x.PostalCode == address.PostalCode))
            {
                var existingPostalCode = await _addressRepository.ReadOneAsync(x => x.PostalCode == address.PostalCode);

                // Checking if the entity with the same postal code has the same AddressLine1 as the new address
                if (existingLine1.AddressLine1 == existingPostalCode.AddressLine1)
                {
                    // Checking if AddressLine2 exists and making sure its not an empty or a null value, then retrieves its entity from the database if true
                    if (await _addressRepository.Existing(x => x.AddressLine2 == address.AddressLine2) && !string.IsNullOrEmpty(address.AddressLine2))
                    {
                        var existingLine2 = await _addressRepository.ReadOneAsync(x => x.AddressLine2 == address.AddressLine2);

                        // If the AddressLine2 in the database is the same as the new adress's AddressLine2, and returns method if true
                        if (existingLine1.AddressLine2 == existingLine2.AddressLine2)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        // Returns false if there is no other identical address in the database
        return false;
    }
}
