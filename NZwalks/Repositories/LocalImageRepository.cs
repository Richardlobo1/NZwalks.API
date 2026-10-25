using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,NZWalksDbContext dbContext)
        {            
            this.webHostEnvironment = webHostEnvironment;
           this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> UploadAsync(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",image.FileName+image.FileExtension);
            using var stream=new FileStream(localFilePath,FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;
            //Add image to the database or any other storage mechanism here if needed
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;

        }
    }
}
