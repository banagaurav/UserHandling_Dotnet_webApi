using Microsoft.AspNetCore.Identity;
using UserHandling.DTOs;
using UserHandling.Models;
using UserHandling.Repositories;

namespace UserHandling.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly MappingService _mappingService;

        public UserService(IUserRepository userRepository, MappingService mappingService)
        {
            _userRepository = userRepository;
            _mappingService = mappingService;
        }

        // Get all users as UserDto (async)
        public async Task<IEnumerable<UserDto>> GetUserDtoAll()
        {
            var users = await _userRepository.GetAllUsersAsync();  // Use async method
            return _mappingService.MapToUserDtoList(users);
        }

        // Get user by Id as UserDto (async)
        public async Task<UserDto> GetUserDtoById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);  // Use async method
            if (user == null) return null;
            return _mappingService.MapToUserDto(user);
        }

        // Other methods can stay the same as they are using async methods
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);  // Use async method
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await _userRepository.AddAsync(user);  // Use async method
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.Username == username); // Find user by username

            if (user == null)
            {
                return null; // User not found
            }

            // Verify password using password hasher
            var result = user.Password;
            if (result == null)
            {
                return null; // Invalid password
            }

            return user; // Return the authenticated user
        }
    }
}
