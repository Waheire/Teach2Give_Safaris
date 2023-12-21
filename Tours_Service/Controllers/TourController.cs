using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tours_Service.Models;
using Tours_Service.Models.Dtos;
using Tours_Service.Services;
using Tours_Service.Services.IService;

namespace Tours_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITour _tourService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public TourController(ITour tour, IMapper mappe)
        {
            _tourService = tour;
            _mapper = mappe;
            _responseDto = new ResponseDto();
        }

        [HttpPost("Add a Tour")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> AddTour(AddTourDto addTourDto)
        {
            try
            {
                var tour = _mapper.Map<Tour>(addTourDto);
                var response = await _tourService.AddNewTourAsync(tour);
                _responseDto.Result = response;
                return Created("", _responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get all Tours")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> GetAllTours() 
        {
            try 
            {
                var tours = await _tourService.GetAllToursAsync();
                if (tours == null) 
                {
                    _responseDto.Result = new ResponseDto();
                    return _responseDto;
                }
                _responseDto.Result = tours;
                return Ok(tours);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
