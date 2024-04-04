using Infrastructure.Data.Context;
using Infrastructure.Models.Identification;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Services;
public class AccountManager(UserManager<ApplicationUser> userManager, IConfiguration configuration, DataContext context, UserRepository userRepository)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly DataContext _context = context;
    private readonly UserRepository _userRepository = userRepository;
    public async Task<bool> UploadProfileImageAsync(IFormFile file, ClaimsPrincipal user)
    {
        try
        {
            if (user != null && file != null && file.Length != 0)
            {
                var userEntity = await _userManager.GetUserAsync(user);
                if (userEntity != null)
                {
                    var fileName = $"p_{userEntity.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!, fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    userEntity.ProfileImage = fileName;

                    var result = await _userRepository.UpdateAsync(u => u.Id == userEntity.Id, userEntity);
                    if (result != null)
                        return true;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
