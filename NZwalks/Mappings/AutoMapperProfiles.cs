using AutoMapper;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<useDTO, domainDTO>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.FullName)).ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, RegionDto>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, RegionDto>().ReverseMap();
            CreateMap<AddWalksRequestDto,Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalksRequestDto, Walk>().ReverseMap();
        }

    }
    //public class useDTO
    //{
    //    public string FullName { get; set; }
    //}
    //public class domainDTO
    //{
    //    public string Name { get; set; }
    //}
    }
