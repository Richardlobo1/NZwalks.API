using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase

    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)   
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm ]ImageUploadRequestDto requets)
        {
            ValidateFileUpload(requets);
            if (ModelState.IsValid)
            {
                var imageDominModel = new Image
                {

                    File = requets.File,
                    FileName = requets.FileName,
                    FileDescription = requets.FileDescription,
                    FileSizeInBytes = requets.File.Length,
                    FileExtension = Path.GetExtension(requets.File.FileName), 

                };
                await imageRepository.UploadAsync(imageDominModel);
                return Ok(imageDominModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto requets)
        {
           var allowedExtensions=new string[]{".jpg",".jpeg",".png"};
            if (!allowedExtensions.Contains(Path.GetExtension(requets.File.FileName)))
            {
                ModelState.AddModelError("File", "Only .jpg, .jpeg, and .png files are allowed.");
            }
            if(requets.File.Length>10 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "File size should not exceed 10 MB.");
            }
        }
    }
}
