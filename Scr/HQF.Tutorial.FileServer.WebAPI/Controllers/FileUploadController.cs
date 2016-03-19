using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HQF.Tutorial.FileServer.WebAPI.Infrastructure;

namespace HQF.Tutorial.FileServer.WebAPI.Controllers
{

    [RoutePrefix("api/Files")]
    public class FileUploadController : ApiController
    {
        private readonly string _uploadFolder = "~/UploadFiles";

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [MimeMultipart]
        [HttpPost]
        [Route("")]
        public async Task<FileUploadResult> Post()
        {
            var uploadPath = HttpContext.Current.Server.MapPath(_uploadFolder);

            var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

            // Read the MIME multipart asynchronously
            await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

            string _localFileName = multipartFormDataStreamProvider
                .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

            // Create response
            return new FileUploadResult
            {
                LocalFilePath = _localFileName,

                FileName = Path.GetFileName(_localFileName),

                FileLength = new FileInfo(_localFileName).Length
            };
        }
    }
}