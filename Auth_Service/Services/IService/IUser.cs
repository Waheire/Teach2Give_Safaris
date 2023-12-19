using Auth_Service.Models.Dtos;

namespace Auth_Service.Services.IService
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterUserDto userDto);

        Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto);

        Task<bool> AssignUserRoles(AddUserRoleDto addUserRoleDto);
    }
}
