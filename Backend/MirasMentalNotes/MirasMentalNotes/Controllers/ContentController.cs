using Microsoft.AspNetCore.Mvc;
using MirasMentalNotes.Models;
using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Controllers
{
    [ApiController]
    [Route("api/content")]
    public class ContentController : ControllerBase
    {
        [HttpGet]
        [Route("{fileName}")]
        public ActionResult<Content> GetContentFile(string fileName)
        {
            var content = Models.Content.FromFileName(fileName);

            return content is not null
                ? content
                : NotFound("The requested file does not exist.");
        }

        [HttpGet]
        public ActionResult<List<string>> GetAllContentFileNames()
        {
            var fileNames = Directory.GetFiles(AppSettings.FileConfig.ContentDirectory!).ToList();
            return GetNamesTrimmedToRelativePaths(fileNames);
        }

        private List<string> GetNamesTrimmedToRelativePaths(List<string> fileNames)
        {
            var contentDir = AppSettings.FileConfig.ContentDirectory!;

            return fileNames.Select(fileName => Path.GetRelativePath(contentDir, fileName)).ToList();
        }
    }
}
