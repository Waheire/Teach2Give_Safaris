using AutoMapper;
using Coupon_Service.Models;
using Coupon_Service.Models.Dtos;
using Coupon_Service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coupon_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICoupon _couponService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public CouponController(ICoupon couponService, IMapper mapper)
        {
            _couponService = couponService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet("AllCoupons")]
        public async Task<ActionResult<ResponseDto>> GetAllCoupons() 
        {
            try 
            {
                var coupons = await _couponService.GetAllCouponsAsync();
                _responseDto.Result = coupons;
                return Ok(_responseDto);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetCouponById(Guid id) 
        {
            try 
            {
                var coupon = await _couponService.GetCouponByIdAsync(id);
                if(coupon == null) 
                {
                    _responseDto.ErrorMessage = "Coupon Not Found";
                    return NotFound(_responseDto);
                }
                _responseDto.Result = coupon;
                return Ok(_responseDto);

            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> AddCoupon(AddCouponDto newCoupon) 
        {
            try 
            {
                var coupon = _mapper.Map<Coupon>(newCoupon);
                var newC = await _couponService.GetCouponByIdAsync(coupon.CouponId);
                if (newC != null) 
                {
                    _responseDto.ErrorMessage = "Coupon Exists";
                    return Ok(_responseDto);
                }
                var response = await _couponService.AddCouponAsync(coupon);
                _responseDto.Result = response;
                return Created("", _responseDto);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(Guid id) 
        {
            try 
            {
                //check if coupon exixts
                var coupon = await _couponService.GetCouponByIdAsync(id);
                if (coupon == null) 
                {
                    _responseDto.ErrorMessage = "Coupon Not Found";
                    return NotFound(_responseDto);
                }

                await _couponService.DeleteCouponAsync(coupon);
                _responseDto.Result = _responseDto;
                return Ok(_responseDto);
            } 
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
