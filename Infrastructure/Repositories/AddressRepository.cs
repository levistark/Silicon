using Infrastructure.Data.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class AddressRepository : Repo<AddressEntity>
{
    private readonly DataContext _dataContext;
    public AddressRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public override async Task<IEnumerable<AddressEntity>> ReadAllAsync()
    {
        try
        {
            return await _dataContext.Addresses.Include(i => i.Users).ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public override async Task<AddressEntity> ReadOneAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {
            var existingEntity = await _dataContext.Addresses.Include(i => i.Users).FirstOrDefaultAsync(predicate)!;
            if (existingEntity != null)
                return existingEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
