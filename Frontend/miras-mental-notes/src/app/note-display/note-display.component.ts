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

  saveDisabled = true;

  constructor(private noteService: NoteService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.noteService.currentNoteChanged.subscribe(note => {
      this.note = note
      this.content.setValue(note.content === undefined ? "" : note.content);
    });

    this.content.valueChanges.subscribe(() => this.checkIfSaveIsDisabled());
  }

  // TODO: figure out why this one won't work with the Lorem ipsum text
  public checkIfSaveIsDisabled(): void {
    if (!this.note.name) {
      this.saveDisabled = true;
      return;
    }
    this.saveDisabled = this.content.value === this.note.content;
  }

  public save(): void {
    if (this.saveDisabled) return;

    const noteToSave: Note = {
      name: this.note.name,
      content: this.content.value!
    };

    this.noteService.save(noteToSave).subscribe(newNote => {
      this.note = newNote;
      this.content.setValue(newNote.content!);
    });
  }
}
