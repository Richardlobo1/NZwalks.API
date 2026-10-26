using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using System.Net;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IwalkRepository iwalkRepository;
        public WalksController(IMapper mapper, IwalkRepository iwalkRepository)
        { this.mapper = mapper; this.iwalkRepository = iwalkRepository; }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            var walkDomianModel = mapper.Map<Walk>(addWalksRequestDto);
            await iwalkRepository.CreateAsync(walkDomianModel);
            return Ok(mapper.Map<WalkDto>(walkDomianModel));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAccending, [FromQuery] int pageNumber = 1,[FromQuery]int pageSize=1000
            )
        {          

            var walksdomianmodel = await iwalkRepository.GetAllAsync(filterOn, filterQuery,sortBy,isAccending,pageNumber,pageSize);
            //
            throw new Exception("This is a test exception for logging purposes.");
            return Ok(mapper.Map<List<WalkDto>>(walksdomianmodel));
               
        }


        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var walkdominmodel = await iwalkRepository.GetByIdAsync(Id);

            if (walkdominmodel == null)
            {
                return NotFound();

            }
            return Ok(mapper.Map<WalkDto>(walkdominmodel));
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {
            var walkdominmodel = mapper.Map<Walk>(updateWalksRequestDto);
            walkdominmodel = await iwalkRepository.UpdateAsync(Id, walkdominmodel);
            if (walkdominmodel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkdominmodel));
        }
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid Id)
        {

            var deletewalkDominmodel = await iwalkRepository.DeleteAsync(Id);
            if (deletewalkDominmodel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletewalkDominmodel));
        }
    }
}
