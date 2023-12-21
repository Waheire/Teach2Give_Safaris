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
        private readonly IJwt _jwtServices;

        public UserService(AppDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IJwt jwtServices)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtServices = jwtServices;
        }


        public async Task<bool> AssignUserRoles(AddUserRoleDto addUserRoleDto)
        {
            // a user with that username exists
            var user = await _context.ApplicationUsers.Where(x => x.Email.ToLower() == addUserRoleDto.Email.ToLower()).FirstOrDefaultAsync();
            //check if user exists
            if (user == null)
            {
                return false;
            }
            else 
            {
                //does the role exist
                if (!_roleManager.RoleExistsAsync(addUserRoleDto.RoleName).GetAwaiter().GetResult()) 
                {
                    //create role
                    await _roleManager.CreateAsync(new IdentityRole(addUserRoleDto.RoleName));
                }

                //assign the user the role
                await _userManager.AddToRoleAsync(user, addUserRoleDto.RoleName);
                return true;
            }
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
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtServices.GenerateToken(user, roles);
            var response = new LoginResponseDto()
            {
                User = loggedUser,
                Token = token
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
                    //does the role exist
                    if (!_roleManager.RoleExistsAsync(userDto.Role).GetAwaiter().GetResult())
                    {
                        //create role
                        await _roleManager.CreateAsync(new IdentityRole(userDto.Role));
                    }

                    //assign the user the role
                    await _userManager.AddToRoleAsync(user, userDto.Role);
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
