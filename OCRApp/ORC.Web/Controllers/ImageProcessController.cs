using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OCRApp;

namespace ORC.Web.Controllers
{
    [RoutePrefix("api/process_image")]
    public class ImageProcessController : ApiController
    {
        // GET api/<controller>
        [Route("{phrase}/coordinates"), HttpGet]
        public IEnumerable<Dictionary<string, object>> GetPhraseCoordinates(string phrase)
        {
            var tessDataPath = HttpContext.Current.Server.MapPath(@"~\tessdata");
            var ENGLISH_LANGUAGE = @"eng";

            var blogPostImage = @"C:\Users\valsh_000\Desktop\document.tif";
            var service = new FindService(blogPostImage, tessDataPath, ENGLISH_LANGUAGE);
            var coords = service.GettCoordinates(phrase);
            return coords;
        }
    }
}