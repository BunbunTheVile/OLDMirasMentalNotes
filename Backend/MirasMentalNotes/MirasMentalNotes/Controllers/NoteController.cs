using Microsoft.AspNetCore.Mvc;
using MirasMentalNotes.Models;
using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Controllers
{
    [ApiController]
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        private string fullPath(string fileName) =>
            Path.Combine(AppSettings.FileConfig.ContentDirectory, fileName);

        [HttpPut]
        public ActionResult<Note> UpdateNote(Note note)
        {
            if (note.File is null || note.Content is null)
                return BadRequest("Both the file name and content need to be defined.");

            var filePath = fullPath(note.File);

            if (!System.IO.File.Exists(filePath))
                return NotFound("A file with that name does not exist.");

            System.IO.File.WriteAllText(filePath, note.Content);

            return GetNote(note.File);
        }

        [HttpDelete]
        [Route("{fileName}")]
        public ActionResult<string> DeleteNote(string fileName)
        {
            var filePath = fullPath(fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("A file with that name does not exist.");

            System.IO.File.Delete(filePath);
            return Ok();
        }

        [HttpPost]
        [Route("{fileName}")]
        public ActionResult<Note> CreateNote(string fileName)
        {
            var filePath = fullPath(fileName);

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
