using Auth_Service.Data;
using Auth_Service.Models;
using Auth_Service.Models.Dtos;
using Auth_Service.Services.IService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth_Service.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(AppDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public Task<bool> AssignUserRoles(AddUserRoleDto addUserRoleDto)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto)
        {
            // a user with thet username exists
            var user = await _context.ApplicationUsers.Where(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower()).FirstOrDefaultAsync();

            //compare hashed password with plain password
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!isValid || user == null) 
            {
                //if username or password or the two are wrong
                return new LoginResponseDto();
            }
            var loggedUser = _mapper.Map<UserResponseDto>(user);
            var response = new LoginResponseDto()
            {
                User = loggedUser,
                Token = "Coming soon....!"
            };
            return response;
        }

        public async Task<string> RegisterUser(RegisterUserDto userDto)
        {
            try 
            {
                var user = _mapper.Map<ApplicationUser>(userDto);

                //create user 
                var response = await _userManager.CreateAsync(user, userDto.password);

                //if succeeded
                if (response.Succeeded)
                {
                    return string.Empty;
                } else 
                {
                    return response.Errors.FirstOrDefault().Description;
                }
                
            } catch (Exception ex) 
            {
                return ex.Message;
            }

            
        }
    }
}
