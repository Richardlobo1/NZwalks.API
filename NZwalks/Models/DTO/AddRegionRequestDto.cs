using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Code must be at least 2 characters long.")]
        [MaxLength(5, ErrorMessage = "Code cannot be more than 5 characters long.")]
        public string Code { get; set; }

        [Required]
        [MinLength(100, ErrorMessage = "Region name must be at least 100 characters long.")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
