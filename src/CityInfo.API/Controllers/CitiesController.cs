using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{


    [ApiController]
  //  [Authorize]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CitiesController> _logger;
        const int maxCitiesPageSize = 20;
        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper,
            ILogger<CitiesController> logger)
        {
            _logger = logger;
            this._cityInfoRepository = cityInfoRepository ?? 
                throw new ArgumentNullException(nameof(cityInfoRepository));
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets a list of cities.
        /// </summary>
        /// <param name="name">name filter</param>
        /// <param name="searchQuery">query to search with</param>
        /// <param name="pageNumber">page number to fetch</param>
        /// <param name="pageSize">size of page</param>
        /// <returns>A list of Cities without Points of interest info.</returns>
        [HttpGet]
        [ReportApiVersions]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(
           [FromQuery] string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }

            _logger.LogInformation("Getting all cities");
            System.Diagnostics.Trace.TraceError("Something bad happened!");
            var (cityEntities, pageMetadata) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", 
                JsonSerializer.Serialize(pageMetadata));

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }


        /// <summary>
        /// Get a city by ID
        /// </summary>
        /// <param name="id">ID of city</param>
        /// <param name="includePointsOfInterest">If we should include the points of interest for city</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns requested city</response>
        /// <response code="404">City was not found</response>
        /// <response code="400">Bad request</response>
        [HttpGet("{id}")]
        [ReportApiVersions]
    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            //var cityToReturn = (citiesData.Cities.FirstOrDefault(c => c.Id == id));

            //if (cityToReturn == null)
            //{
            //    return NotFound();
            //}

            //return Ok(cityToReturn);
           
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var result = _mapper.Map<CityDto>(city);
                return Ok(result);
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}
