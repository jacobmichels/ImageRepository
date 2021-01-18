using ImageRepository.Server.Data;
using ImageRepository.Server.Models;
using ImageRepository.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepository.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : Controller
    {
        private readonly ILogger<ImageController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public AlbumController(ILogger<ImageController> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbumsForUser()
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            logger.LogInformation("Returning albums.");
            return dbContext.Albums.Where(album => album.OwnerID == user.Id).ToList();
        }
        [HttpGet]
        [Route("GetAlbumImages/{id}")]
        public async Task<ActionResult<IEnumerable<Image>>> GetImagesForAlbum(string id)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            Album album = dbContext.Albums.First(album=>album.AlbumID==id);
            logger.LogInformation("Returning images belonging to the album.");
            return dbContext.Images.Where(image => album.ImagesIDs.Contains(image.ImageId)).ToList();
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewAlbum(Album album)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            album.OwnerID = user.Id;
            try
            {
                dbContext.Albums.Add(album);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                logger.LogInformation($"Album failed to save. Message:{e.Message}");
                return Problem(detail: "Error saving album.", statusCode: 500);
            }
            logger.LogInformation("Album saved.");
            return Ok();
        }
        [HttpPost]
        [Route("rename")]
        public async Task<IActionResult> RenameAlbum(Album updatedAlbum)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            try
            {
                dbContext.Albums.First(album => album.AlbumID == updatedAlbum.AlbumID).AlbumName=updatedAlbum.AlbumName;
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogInformation($"Album failed to update. Message:{e.Message}");
                return Problem(detail: "Error updating album.", statusCode: 500);
            }
            logger.LogInformation("Album updated.");
            return Ok();
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteAlbum(Album albumToDelete)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            
            try
            {
                dbContext.Albums.Remove(dbContext.Albums.First(album => album.AlbumID == albumToDelete.AlbumID));
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogInformation($"Album failed to delete. Message:{e.Message}");
                return Problem(detail: "Error deleting album.", statusCode: 500);
            }
            logger.LogInformation("Album deleted.");
            return Ok();
        }
        [HttpPost]
        [Route("deleteimages")]
        public async Task<IActionResult> DeleteImages(Album updatedAlbum)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }

            try
            {
                dbContext.Albums.First(album => album.AlbumID == updatedAlbum.AlbumID).ImagesIDs = updatedAlbum.ImagesIDs;
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogInformation($"Album images failed to delete. Message:{e.Message}");
                return Problem(detail: "Error deleting album images.", statusCode: 500);
            }
            logger.LogInformation("Images removed from album.");
            return Ok();
        }
        [HttpPost]
        [Route("addimages")]
        public async Task<IActionResult> AddImages(Album updatedAlbum)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                logger.LogError("Could not find current user");
                return Problem(detail: "Error finding current user.", statusCode: 500);
            }
            try
            {
                dbContext.Albums.First(album => album.AlbumID == updatedAlbum.AlbumID).ImagesIDs = updatedAlbum.ImagesIDs;
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogInformation($"Album images failed to add. Message:{e.Message}");
                return Problem(detail: "Error adding album images.", statusCode: 500);
            }
            logger.LogInformation("Images added to album.");
            return Ok();
        }
    }
}
