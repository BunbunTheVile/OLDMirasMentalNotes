import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NoteListItem } from '../models/note.model';
import { NoteService } from '../services/note.service';

@Component({
  selector: 'app-note-list',
  templateUrl: './note-list.component.html',
  styleUrls: ['./note-list.component.css']
})
export class NoteListComponent implements OnInit {
  noteListItems: NoteListItem[] = [];
  fileInput = new FormControl<string>("");

  constructor(private noteService: NoteService, private router: Router) { }

  ngOnInit(): void {
    this.noteService.getNames().subscribe(names => {
      names.forEach(name => {
        this.noteListItems.push({
          file: name,
          deletionStarted: false
        });
      });
    });
  }

  public select(item: NoteListItem): void {
    this.noteService.select(item.file);
  }

  public create(): void {
    const fileName = this.fileInput.getRawValue();

    if (!fileName || fileName === "") return;

    this.noteService.create(fileName).subscribe(note => {
      if (note.file) {
        const listItem: NoteListItem = {
          file: note.file,
          deletionStarted: false
        }

        this.noteListItems.push(listItem);

        this.select(listItem);
      }

      this.fileInput.setValue("");
    });
  }

  public delete(item: NoteListItem): void {
    this.noteService.delete(item.file).subscribe(() => {
      const index = this.noteListItems.indexOf(item);
      
      if (index !== -1)
        this.noteListItems.splice(index, 1);
    });
  }

  public beginDeletion(item: NoteListItem): void {
    item.deletionStarted = true;
  }

  public stopDeletion(item: NoteListItem): void {
    item.deletionStarted = false;
  }
}
