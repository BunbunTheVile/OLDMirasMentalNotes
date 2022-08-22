import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from 'src/app/models/note.model';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  backendUrl: string = "https://localhost:7001/api/note";

  currentNote: Note = {};

  @Output()
  currentNoteChanged = new EventEmitter<Note>();

  constructor(private httpClient: HttpClient) {}

  public getNames(): Observable<string[]>  {
    return this.httpClient.get<string[]>(this.backendUrl);
  }

  public get(fileName: string): Observable<Note> {
    return this.httpClient.get<Note>(`${this.backendUrl}/${fileName}`)
  }

  public select(fileName: string): void {
    this.get(fileName).subscribe(note => {
      this.currentNote = note;
      this.currentNoteChanged.emit(note);
    });
  }

  public create(fileName: string): Observable<Note> {
    return this.httpClient.post<Note>(`${this.backendUrl}/${fileName}`, null);
  }
}
