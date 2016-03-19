using System.Collections.Generic;
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
        public async Task<IEnumerable<FileUploadResult>> Post()
        {
            var uploadPath = HttpContext.Current.Server.MapPath(_uploadFolder);

            var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

            // Read the MIME multipart asynchronously
            await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

          var fileInfo = multipartFormDataStreamProvider.FileData.Select(i => {
                var info = new FileInfo(i.LocalFileName);
                return new FileUploadResult() {
                    FileName = info.Name,
                    LocalFilePath = uploadPath + "/" + _uploadFolder + "/" + info.Name,
                    FileLength = info.Length / 1024};
            });


            // Create response
            return fileInfo; ;
        }
    }
}