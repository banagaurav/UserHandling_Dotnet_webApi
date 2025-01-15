using System;
using UserHandling.Models;

namespace UserHandling.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> AddAsync(User user);
}