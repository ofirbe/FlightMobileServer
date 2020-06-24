using System.Net;
using FlightMobileApp.Model.Manager;
using Microsoft.AspNetCore.Mvc;

namespace FlightMobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenShotController : ControllerBase
    {
        private readonly ScreenshotManager manager;
        public ScreenShotController(ScreenshotManager manager)
        {
            this.manager = manager;
        }

        // GET: /screenshot
        [HttpGet]
        [Route("/screenshot/")]
        public IActionResult Get()
        {
            string url = "http://localhost:" + manager.PORT.ToString() + "/screenshot";
            byte[] imageBytes = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    imageBytes = webClient.DownloadData(url);
                }
                return File(imageBytes, "image/jpeg");
            }
            catch
            {
                return StatusCode(418);
            }
        }
    }
}