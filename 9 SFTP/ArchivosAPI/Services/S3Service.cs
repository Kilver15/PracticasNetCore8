using Amazon.S3;
using Amazon.S3.Transfer;

namespace ArchivosAPI.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            _bucketName = configuration["AWS:BucketName"];

            // Crear el cliente de S3
            _s3Client = new AmazonS3Client(
                configuration["AWS:AccessKey"],
                configuration["AWS:SecretKey"],
                Amazon.RegionEndpoint.GetBySystemName(configuration["AWS:Region"])
            );
        }

        // Subir archivo a S3
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);

            using (var fileStream = file.OpenReadStream())
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = fileStream,
                    Key = file.FileName,
                    BucketName = _bucketName,
                };

                await fileTransferUtility.UploadAsync(uploadRequest);

                return $"https://{_bucketName}.s3.amazonaws.com/{file.FileName}";
            }
        }

        // Descargar archivo de S3
        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, fileName);
            return response.ResponseStream;
        }
    }
}
