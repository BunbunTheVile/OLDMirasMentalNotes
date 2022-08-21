import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NoteService } from '../services/note.service';

@Component({
  selector: 'app-note-list',
  templateUrl: './note-list.component.html',
  styleUrls: ['./note-list.component.css']
})
export class NoteListComponent implements OnInit {
  files: string[] = [];
  fileInput = new FormControl<string>("");

  constructor(private noteService: NoteService, private router: Router) { }

  ngOnInit(): void {
    this.noteService.getNames().subscribe(names => this.files = names);
  }

  public select(file: string): void {
    this.noteService.select(file);
  }

  public create(): void {
    const fileName = this.fileInput.getRawValue();

    if (!fileName) return;

    this.noteService.create(fileName).subscribe(note => {
      if (note.file)
        this.files.push(note.file);
        this.fileInput.setValue("");
    });
  }
}
