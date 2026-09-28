using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext DbContext;

        public RegionsController(NZWalksDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
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
            var regions = DbContext.Regions.ToList();
            //Mapm domian model to DTO
            var regionDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                    Id = region.Id,

                });
            }

            return Ok(regionDto);

        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public IActionResult GetRegionById([FromRoute] Guid Id)
        {
            // var regions = DbContext.Regions.Find(Id);
            //Get data from database- domian model
            var region = DbContext.Regions.FirstOrDefault(x => x.Id == Id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
                Id = region.Id,
            };


            return Ok(regionDto);

        }
        [HttpPost]
        public IActionResult AddRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to Domain model
            var region = new Region()
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };
            Console.WriteLine(region);
            //Save to database
            DbContext.Regions.Add(region);
            DbContext.SaveChanges();
            //Map Domain model to DTO
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return CreatedAtAction(nameof(GetRegionById), new { Id = regionDto.Id }, regionDto);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //checkif region exits
            var regionDominmodel = DbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDominmodel == null)
            {
                return NotFound();
            }
            //Map DTO to Domain model

            regionDominmodel.Name = updateRegionRequestDto.Name;
            regionDominmodel.Code = updateRegionRequestDto.Code;
            regionDominmodel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            DbContext.SaveChanges();

            var regionDTo = new RegionDto
            {
                Id = regionDominmodel.Id,
                Name = regionDominmodel.Name,
                Code = regionDominmodel.Code,
                RegionImageUrl = regionDominmodel.RegionImageUrl
            };

            return Ok(regionDTo);
        }

        //Delete Region
        [HttpDelete]
        [Route("{Id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid Id)
        {
            var region = DbContext.Regions.FirstOrDefault(x => x.Id == Id);
            if (region == null)
            {
                return NotFound();
            }
            DbContext.Regions.Remove(region);
            DbContext.SaveChanges();
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);


        }
    }
}
