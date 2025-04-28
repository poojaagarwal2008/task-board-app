using Microsoft.AspNetCore.Mvc;
using FileService.Application.Interfaces;

namespace FileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FilesController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        // Upload a file
        [HttpPost("upload")]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var url = await _fileUploadService.UploadFileAsync(file);
            return Ok(url);
        }
    }
}
