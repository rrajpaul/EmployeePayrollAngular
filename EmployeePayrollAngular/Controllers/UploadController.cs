using System.IO;
using System.Net.Http.Headers;
using EmployeePayrollAngular.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeePayrollAngular.Controllers
{
    [Route("api/upload")]
    public class UploadController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        IConfiguration _iconfiguration;
        public UploadController(IConfiguration iconfiguration, IHostingEnvironment hostingEnvironment)
        {
            _iconfiguration = iconfiguration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpPost, DisableRequestSizeLimit]
        public string UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                string fullPath = "";
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }                   
                }

                string conStr = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

                var errorMsg = ProcessUploadedFile.GetDataTableFromCSVFile(fullPath, conStr, 4);

                return errorMsg;
            }
            catch (System.Exception ex)
            {        
                return ex.Message;
            }
        }
    }
}