using UserHandling.Dtos;
using UserHandling.DTOs;
using UserHandling.Models;

public class MappingService
{
    public UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
    // Method to map CreateUserDto to User entity
    public User MapToUser(CreateUserDto createUserDto)
    {
        return new User
        {
            Username = createUserDto.Username,
            Password = createUserDto.Password, // You may want to hash the password before saving
            Email = createUserDto.Email,
            Role = createUserDto.Role,
            CreatedAt = DateTime.UtcNow // Set the creation date
        };
    }
    public IEnumerable<UserDto> MapToUserDtoList(IEnumerable<User> users)
    {
        return users.Select(u => MapToUserDto(u));
    }


    public IEnumerable<PdfDto> MapToPdfDtos(IEnumerable<Pdf> pdfs)
    {
        return pdfs.Select(pdf => new PdfDto
        {
            Title = pdf.Title,
            Description = pdf.Description,
            Authors = pdf.UserPdfs?.Select(up => up.User.Username).ToList() ?? new List<string>()
        }).ToList();
    }

    public PdfUploadDto MapToPdfUploadDto(Pdf pdf)
    {
        return new PdfUploadDto
        {
            PdfId = pdf.PdfId,
            Title = pdf.Title,
            Description = pdf.Description,
            FilePath = pdf.FilePath,
            Authors = pdf.UserPdfs?.Select(up => up.User.Username).ToList() ?? new List<string>()
        };
    }
}


