using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImageRepository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ImageRepository.Server.Models;
using ImageRepository.Server.Data;

namespace ImageRepository.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public ImageController(ILogger<ImageController> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IEnumerable<Image> Images)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail:"Error finding current user.",statusCode:500);
            }
            if (!Images.Any())
            {
                return BadRequest("No images uploaded.");
            }

            int count = 0;
            foreach(Image image in Images)
            {
                image.UserID = user.Id;
                dbContext.Images.Add(image);
                count++;
            }
            user.ImageCount += count;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError($"Exception thrown saving changes: {e.Message}");
                return Problem(detail: "Error saving changes.", statusCode: 500);
            }
            logger.LogInformation("Image uploaded successfully.");
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImagesForUser()
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            logger.LogInformation("Returning images.");
            return dbContext.Images.Where(image => image.UserID == user.Id).ToList();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteImages(IEnumerable<string> ImageIDsToDelete)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            dbContext.Images.RemoveRange(dbContext.Images.Where(image => ImageIDsToDelete.Contains(image.ImageId)));
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError($"Exception thrown saving changes: {e.Message}");
                return Problem(detail: "Error saving changes.", statusCode: 500);
            }
            logger.LogInformation("Image deleted.");
            return Ok();
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateImage(Image UpdatedImage)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            Image toUpdate = dbContext.Images.FirstOrDefault(DbImage => DbImage.ImageId == UpdatedImage.ImageId);
            if (toUpdate == null)
            {
                logger.LogError("Image to update not found.");
                return Problem(detail: "Error finding at least one of the image to update.");
            }
            toUpdate.Title = UpdatedImage.Title;
            toUpdate.Caption = UpdatedImage.Caption;
            toUpdate.Private = UpdatedImage.Private;
            
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError($"Exception thrown saving changes: {e.Message}");
                return Problem(detail: "Error saving changes.", statusCode: 500);
            }
            logger.LogInformation("Image updated.");
            return Ok();
        }
    }
}
