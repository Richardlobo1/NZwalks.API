using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.CustomActionFilter;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext DbContext;
        private readonly IRegionRepositotory regionRepositotory;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext DbContext,IRegionRepositotory regionRepositotory,IMapper mapper)
        {
            this.DbContext = DbContext;
            this.regionRepositotory = regionRepositotory;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles ="Reader,Writer")]

        public async Task<IActionResult> GetAll()
        {

            //var region = new List<Region>
            //{
            //    new Region
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="Asia",
            //        Code="AS",
            //        RegionImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4W5se-3sXcI-CuvSm5GbPoSk655stnvqEeWyX1M79KA&s=10"

            //    },
            //      new Region
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="United State",
            //        Code="USA",
            //        RegionImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4W5se-3sXcI-CuvSm5GbPoSk655stnvqEeWyX1M79KA&s=10"

            //    },
            //};
            //Get data from database- domian model
            var regions = await regionRepositotory.GetAllAsync(); 
            //Mapm domian model to DTO
                //var regionDto = new List<RegionDto>();
                //foreach (var region in regions)
                //{
                //    regionDto.Add(new RegionDto()
                //    {
                //        Name = region.Name,
                //        Code = region.Code,
                //        RegionImageUrl = region.RegionImageUrl,
                //        Id = region.Id,

                //    });
                //}

            var regionDto=mapper.Map<List<RegionDto>>(regions);
            return Ok(regionDto);

        }
        [HttpGet]

        [Authorize(Roles = "Reader,Writer")]

        [Route("{Id:Guid}")]

        public async Task<IActionResult> GetRegionById([FromRoute] Guid Id)
        {
            // var regions = DbContext.Regions.Find(Id);
            //Get data from database- domian model
            var region = await regionRepositotory.GetByIdAsync(Id);
            if (region == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto()
            //{
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,
            //    Id = region.Id,
            //};


            return Ok(mapper.Map<RegionDto>(region));

        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> AddRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to Domain model
            //var region = new Region()
            //{
            //    Name = addRegionRequestDto.Name,
            //    Code = addRegionRequestDto.Code,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            //};


            //by autompper
            var region = mapper.Map<Region>(addRegionRequestDto);
            //Save to database
            //await DbContext.Regions.AddAsync(region);
            //DbContext.SaveChangesAsync();
           region= await regionRepositotory.CreateRegionAsync(region);
            //Map Domain model to DTO
            //var regionDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,
            //};
            var regionDto = mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetRegionById), new { Id = regionDto.Id }, regionDto);

        }
        
        
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            //Validate the request
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //checkif region exits
            //var regionDominmodel = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //if (regionDominmodel == null)
            //{
            //    return NotFound();
            //}
            //Map DTO to Domain model

            //regionDominmodel.Name = updateRegionRequestDto.Name;
            //regionDominmodel.Code = updateRegionRequestDto.Code;
            //regionDominmodel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            //await DbContext.SaveChangesAsync();
            //var updatedRegion = await regionRepositotory.UpdateRegionAsync(id, new Region
            //{

            //    Name = updateRegionRequestDto.Name,
            //    Code = updateRegionRequestDto.Code,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //});

            //by automapper
            var updatedRegion = mapper.Map<RegionDto>(updateRegionRequestDto);
            updatedRegion = await regionRepositotory.UpdateRegionAsync(id, updatedRegion);
            if (updatedRegion == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(updatedRegion));
        }

        //Delete Region
        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid Id)
        {
            //Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var region = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            //if (region == null)
            //{
            //    return NotFound();
            //}
            //DbContext.Regions.Remove(region);
            //await DbContext.SaveChangesAsync();
            //var regionDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,
            //};
            //return Ok(regionDto);
            var region = await regionRepositotory.DeleteRegionAsync(Id);
            if (region == null)
            {
                return NotFound();


            }
            return Ok(mapper.Map<RegionDto>(region));
        }
        }
}
