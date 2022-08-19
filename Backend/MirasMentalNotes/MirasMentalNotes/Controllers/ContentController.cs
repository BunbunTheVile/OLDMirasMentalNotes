using Microsoft.AspNetCore.Mvc;
using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Controllers
{
    [ApiController]
    [Route("api/content")]
    public class ContentController : ControllerBase
    {
        [HttpGet]
        public List<string> GetAllContentFileNames()
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
