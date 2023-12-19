using Auth_Service.Models.Dtos;
using Auth_Service.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _responseDto;

        public UserController(IUser userService)
        {
            _userService = userService;
            _responseDto = new ResponseDto(); 
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto) 
        {
            var res = await _userService.RegisterUser(registerUserDto);
            if (string.IsNullOrWhiteSpace(res)) 
            {
                //this was success
                _responseDto.Result = "User Registered Successfully";
                return Created("", _responseDto);
            }
            _responseDto.ErrorMessage = res;
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto) 
        {
            var res = await _userService.LoginUser(loginRequestDto);
            if (res.User!=null)
            {
                //this was success
                _responseDto.Result = res;
                return Created("", _responseDto);
            }
            _responseDto.ErrorMessage = "Invalid Credentials";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
    }
}
