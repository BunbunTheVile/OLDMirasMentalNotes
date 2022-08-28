using Microsoft.AspNetCore.Mvc;
using MirasMentalNotes.Models;
using MirasMentalNotes.Settings;

namespace MirasMentalNotes.Controllers
{
    [ApiController]
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        private string fullPath(string noteName) =>
            Path.Combine(AppSettings.FileConfig.ContentDirectory, noteName) + ".note";

        [HttpPut]
        public ActionResult<Note> UpdateNote(Note note)
        {
            if (note.Name is null || note.Content is null)
                return BadRequest("Both the file name and content need to be defined.");

            var filePath = fullPath(note.Name);

            if (!System.IO.File.Exists(filePath))
                return NotFound("A file with that name does not exist.");

            System.IO.File.WriteAllText(filePath, note.Content);

            return GetNote(note.Name);
        }

        [HttpDelete]
        [Route("{noteName}")]
        public ActionResult<string> DeleteNote(string noteName)
        {
            var filePath = fullPath(noteName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("A file with that name does not exist.");

            System.IO.File.Delete(filePath);
            return Ok();
        }

        [HttpPost]
        [Route("{noteName}")]
        public ActionResult<Note> CreateNote(string noteName)
        {
            var filePath = fullPath(noteName);

            if (System.IO.File.Exists(filePath))
                return BadRequest("A file with that name already exists.");

            System.IO.File.WriteAllText(filePath, "");
            return CreatedAtAction(nameof(GetNote), new { noteName }, new Note { Name = noteName });
        }

        [HttpGet]
        [Route("{noteName}")]
        public ActionResult<Note> GetNote(string noteName)
        {
            var note = Note.FromNoteName(noteName);

            return note is not null
                ? note
                : NotFound("The requested file does not exist.");
        }

        [HttpGet]
        public ActionResult<List<string>> GetAllNoteNames()
        {
            var fileNames = Directory.GetFiles(AppSettings.FileConfig.ContentDirectory!).ToList();
            var noteFiles = fileNames.Where(x => x.EndsWith(".note")).ToList();
            return GetTrimmedNoteNames(noteFiles);
        }

        private List<string> GetTrimmedNoteNames(List<string> fileNames)
        {
            var contentDir = AppSettings.FileConfig.ContentDirectory!;

            var relativePaths = fileNames.Select(fileName => Path.GetRelativePath(contentDir, fileName));

            return relativePaths.Select(x => x.Replace(".note", "")).ToList();
        }
    }
}
