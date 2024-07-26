using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Localization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration, IStringLocalizer<HomeController> localizer)
        {
            _configuration = configuration;
            _localizer = localizer;

        }
        

        [HttpGet("translate")]
        public IActionResult Translate(string word, string lang= "en-US")
        {

            var culture = new CultureInfo(lang);
            var localizer = new JsonStringLocalizer(_configuration, culture, "HomeController");

            var translation = localizer[word.ToLower()];
            return Ok(translation);
        }
    }
}
