using Infrastructure.Data.Context;
using Infrastructure.Entities.Course;

namespace Infrastructure.Repositories;
public class AuthorRepository : Repo<AuthorEntity>
{
    private readonly DataContext _dataContext;
    public AuthorRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
}