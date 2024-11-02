using ArchivosAPI.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly S3Service _s3Service;

        public FilesController(S3Service s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se proporcionó un archivo o está vacío.");
            }

            var fileUrl = await _s3Service.UploadFileAsync(file);

            return Ok(new { Message = "Archivo subido exitosamente", FileUrl = fileUrl });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var fileStream = await _s3Service.DownloadFileAsync(fileName);

            if (fileStream == null)
            {
                return NotFound("El archivo no fue encontrado.");
            }

            var contentType = "application/octet-stream"; // Valor predeterminado
            if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                contentType = "image/png";
            else if (fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                     fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                contentType = "image/jpeg";
            else if (fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                contentType = "application/pdf";

            return File(fileStream, contentType, fileName);
        }
    }
}
