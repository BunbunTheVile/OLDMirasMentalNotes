import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Note } from '../models/note.model';
import { NoteService } from '../services/note.service';

@Component({
  selector: 'app-note-display',
  templateUrl: './note-display.component.html',
  styleUrls: ['./note-display.component.css']
})
export class NoteDisplayComponent implements OnInit {
  
  note: Note = {};
  content = new FormControl<string>("");
  tags = new FormControl<string>("");

  saveDisabled = true;

  constructor(private noteService: NoteService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.noteService.currentNoteChanged.subscribe(note => {
      this.note = note
      
      this.content.setValue(note.content === undefined ? "" : note.content);
      
      this.tags.setValue(this.getTagText(note));
    });

    this.content.valueChanges.subscribe(() => this.checkIfSaveIsDisabled());
    this.tags.valueChanges.subscribe(() => this.checkIfSaveIsDisabled());
  }

  public checkIfSaveIsDisabled(): void {
    if (!this.note.name) {
      this.saveDisabled = true;
      return;
    }
    
    const contentUnchanged = this.content.value === this.note.content;
    const tagsUnchanged = this.tags.value == this.getTagText(this.note);

    this.saveDisabled = contentUnchanged && tagsUnchanged;
  }

  public save(): void {
    if (this.saveDisabled) return;

    const noteToSave: Note = {
      name: this.note.name,
      content: this.content.value!,
      tags: this.tags.value!.split(" ")
    };

    this.noteService.save(noteToSave).subscribe(newNote => {
      this.note = newNote;
      this.content.setValue(newNote.content!);
      this.tags.setValue(this.getTagText(newNote));
    });
  }

  private getTagText(note: Note): string {
      let tagText = "";
      note.tags?.forEach(x => tagText += `${x} `);
      tagText = tagText.trim();
      return tagText;
  }
}
