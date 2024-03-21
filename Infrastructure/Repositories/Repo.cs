using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _dataContext;

    protected Repo(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    // Create
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            var result = await _dataContext.Set<TEntity>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();

            if (result.Entity == entity)
            {
                return result.Entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    // Read 
    public virtual async Task<TEntity> ReadOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var existingEntity = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate)!;
            if (existingEntity != null)
                return existingEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync()
    {
        try
        {
            return await _dataContext.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    // Update
    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var existingEntity = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(expression);

            if (existingEntity != null)
            {
                _dataContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _dataContext.SaveChangesAsync();
                return existingEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    // Delete
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var existingEntity = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(expression);

            if (existingEntity != null && existingEntity == entity)
            {
                _dataContext.Set<TEntity>().Remove(entity);
                await _dataContext.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public virtual async Task<bool> Existing(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await _dataContext.Set<TEntity>().AnyAsync(expression);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
