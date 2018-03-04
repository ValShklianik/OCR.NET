using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using OCRApp;

namespace ORC.Web.Controllers
{
    [RoutePrefix("api/process_image")]
    public class ImageProcessController : ApiController
    {
        [Route("{phrase}/coordinates"), HttpGet]
        public IEnumerable<Dictionary<string, object>> GetPhraseCoordinates(string phrase)
        {
            var tessDataPath = HttpContext.Current.Server.MapPath(@"~\tessdata");
            var englishLanguage = @"eng";

            var blogPostImage = HttpContext.Current.Server.MapPath(@"~\Images\2.png");
            var service = new FindService(blogPostImage, tessDataPath, englishLanguage);
            var coords = service.GettCoordinates(phrase);
            return coords;
        }
    }
}