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

  constructor(private noteService: NoteService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.noteService.currentNoteChanged.subscribe(note => {
      this.note = note
      this.content.setValue(note.content === undefined ? null : note.content);
    });
  }
}
