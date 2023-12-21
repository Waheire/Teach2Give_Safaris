using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tours_Service.Models;
using Tours_Service.Models.Dtos;
using Tours_Service.Services.IService;

namespace Tours_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourImageController : ControllerBase
    {

        private readonly ITour _tourService;
        private readonly ITourImage _tourImageService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;

        public TourImageController(ITour tour, ITourImage tourImage, IMapper mapper)
        {
            _mapper = mapper;
            _tourService = tour;
            _tourImageService = tourImage;
            _responseDto = new ResponseDto();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ResponseDto>> AddImage(Guid id, AddTourImageDto addTourImageDto) 
        {
            try 
            {
                var tour = await _tourService.GetTourByIdAsync(id);
                if (tour == null)
                {
                    _responseDto.ErrorMessage = "Tour Not Found";
                    return NotFound(_responseDto);
                }

                var image = _mapper.Map<TourImage>(addTourImageDto);
                var response = await _tourImageService.AddTourImageAsync(id, image);
                _responseDto.Result = response;
                return Created("", _responseDto);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }
    }
}
