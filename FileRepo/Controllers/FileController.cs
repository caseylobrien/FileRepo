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
        public HttpResponseMessage Download(string fileName)
        {
            StringBuilder sb = new StringBuilder(_root);
            sb.Append(fileName);
            if (!File.Exists(sb.ToString()))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            FileStream fileStream = new FileStream(sb.ToString(), FileMode.Open, FileAccess.Read);
            string contentType = MimeMapping.GetMimeMapping(fileName);


            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return response;
        }
    }
}
