using ImageRepository.Server.Data;
using ImageRepository.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepository.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicImageController : Controller
    {
        private readonly ILogger<PublicImageController> logger;
        private readonly ApplicationDbContext dbContext;

        public PublicImageController(ApplicationDbContext dbContext,
            ILogger<PublicImageController> logger)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Image>> GetPublicImages()
        {
            logger.LogInformation("Returning images.");
            return dbContext.Images.Where(image => image.Private == false).ToList();
        }
    }
}
