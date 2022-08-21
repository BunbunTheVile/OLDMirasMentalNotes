import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NoteService } from '../services/note.service';

@Component({
  selector: 'app-note-list',
  templateUrl: './note-list.component.html',
  styleUrls: ['./note-list.component.css']
})
export class NoteListComponent implements OnInit {
  files: string[] = [];

  constructor(private noteService: NoteService, private router: Router) { }

  ngOnInit(): void {
    this.noteService.getNames().subscribe(names => this.files = names);
  }

  public select(file: string): void {
    this.noteService.select(file);
  }
}
