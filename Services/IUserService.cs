using System;
using UserHandling.DTOs;
using UserHandling.Models;

namespace UserHandling.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUserDtoAll();
    Task<UserDto> GetUserDtoById(int id);
    Task<User> GetUserByIdAsync(int id);
    Task<User> AddUserAsync(User user);

}
