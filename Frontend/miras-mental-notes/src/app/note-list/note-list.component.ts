import { Component, OnInit } from '@angular/core';
import { NoteService } from '../services/note.service';

@Component({
  selector: 'app-note-list',
  templateUrl: './note-list.component.html',
  styleUrls: ['./note-list.component.css']
})
export class NoteListComponent implements OnInit {
  files: string[] = [];

  constructor(private noteService: NoteService) { }

  ngOnInit(): void {
    this.noteService.GetNames().subscribe(names => this.files = names);
  }

}
