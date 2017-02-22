using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FileRepo.Controllers
{
    public class FileController : ApiController
    {
        private const string _root = "C:\\FileApiContent\\";
        // /api/file/download/2016holidays.pdf
        [HttpGet]
        public HttpResponseMessage Download(string filename)
        {
            StringBuilder sb = new StringBuilder(_root);
            sb.Append(filename);
            if (!File.Exists(sb.ToString()))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            FileStream fileStream = new FileStream(sb.ToString(), FileMode.Open, FileAccess.Read);
            string contentType = MimeMapping.GetMimeMapping(filename);


            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = filename;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return response;
        }

        [HttpGet]
        public string test(string filename)
        {
            return "<div>" + filename + "</div>";
        }
    }
}
