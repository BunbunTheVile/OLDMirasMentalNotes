using Microsoft.AspNetCore.Mvc;
using MirasMentalNotes.Models;
using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Controllers
{
    [ApiController]
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        [HttpPost]
        [Route("{fileName}")]
        public ActionResult<Note> CreateNote(string fileName)
        {
            var filePath = Path.Combine(AppSettings.FileConfig.ContentDirectory, fileName);

            if (System.IO.File.Exists(filePath))
                return BadRequest("A file with that name already exists.");

            System.IO.File.WriteAllText(filePath, "");
            return CreatedAtAction(nameof(GetNote), new { fileName }, new Note { File = fileName });
        }

        [HttpGet]
        [Route("{fileName}")]
        public ActionResult<Note> GetNote(string fileName)
        {
            var note = Note.FromFileName(fileName);

            return note is not null
                ? note
                : NotFound("The requested file does not exist.");
        }

        [HttpGet]
        public ActionResult<List<string>> GetAllNoteNames()
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
