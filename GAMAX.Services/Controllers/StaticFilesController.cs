using DataBase.EF.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAMAX.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticFilesController : ControllerBase
    {
        private readonly string directoryPath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
        
        [HttpGet("download")]
        public IActionResult DownloadFile(string filePath)
        {
            //filePath = filePath.Replace("\\\\", "\\");
            var FullPath =Path.Combine(directoryPath, filePath);
            if (string.IsNullOrEmpty(FullPath) || !System.IO.File.Exists(FullPath))
            {
                return NotFound(); // Return 404 Not Found if the file doesn't exist
            }

            // Get the file extension from the file path
            string fileExtension = Path.GetExtension(FullPath).ToLower();

            // Set the content type based on the file extension
            string contentType;
            if (IsImageExtension(fileExtension))
            {
                contentType = "image/" + fileExtension.Substring(1);
            }
            else if (IsVideoExtension(fileExtension))
            {
                contentType = "video/" + fileExtension.Substring(1);
            }
            else
            {
                return BadRequest("Invalid file extension."); // Return a bad request if the file extension is not supported
            }

            // Read the file bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(FullPath);

            // Create a file content result
            var fileContentResult = new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = Path.GetFileName(FullPath) // Set the file name for downloading
            };

            return fileContentResult;
        }
        [HttpGet("downloadProfilePhoto")]
        public IActionResult downloadProfilePhoto(string UserID)
        {
            var filePath = Path.Combine(directoryPath, "StaticFiles", "Photos", "ProfilePhoto",UserID+ "Profile.jpg");
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 Not Found if the file doesn't exist
            }

            // Get the file extension from the file path
            string fileExtension = Path.GetExtension(filePath).ToLower();

            // Set the content type based on the file extension
            string contentType;
            if (IsImageExtension(fileExtension))
            {
                contentType = "image/" + fileExtension.Substring(1);
            }
            else if (IsVideoExtension(fileExtension))
            {
                contentType = "video/" + fileExtension.Substring(1);
            }
            else
            {
                return BadRequest("Invalid file extension."); // Return a bad request if the file extension is not supported
            }

            // Read the file bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Create a file content result
            var fileContentResult = new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = Path.GetFileName(filePath) // Set the file name for downloading
            };

            return fileContentResult;
        }
        [HttpGet("downloadCoverPhoto")]
        public IActionResult downloadCoverPhoto(string UserID)
        {
            var filePath = Path.Combine(directoryPath, "StaticFiles", "Photos", "CoverPhoto", UserID + "Cover.jpg");
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound(); // Return 404 Not Found if the file doesn't exist
            }

            // Get the file extension from the file path
            string fileExtension = Path.GetExtension(filePath).ToLower();

            // Set the content type based on the file extension
            string contentType;
            if (IsImageExtension(fileExtension))
            {
                contentType = "image/" + fileExtension.Substring(1);
            }
            else if (IsVideoExtension(fileExtension))
            {
                contentType = "video/" + fileExtension.Substring(1);
            }
            else
            {
                return BadRequest("Invalid file extension."); // Return a bad request if the file extension is not supported
            }

            // Read the file bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Create a file content result
            var fileContentResult = new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = Path.GetFileName(filePath) // Set the file name for downloading
            };

            return fileContentResult;
        }

        private bool IsImageExtension(string fileExtension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Add more image extensions as needed
            return imageExtensions.Contains(fileExtension);
        }

        private bool IsVideoExtension(string fileExtension)
        {
            string[] videoExtensions = { ".mp4", ".avi", ".mov", ".wmv" }; // Add more video extensions as needed
            return videoExtensions.Contains(fileExtension);
        }

    }
}
