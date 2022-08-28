import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Note } from 'src/app/models/note.model';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  backendUrl: string = "http://localhost:5000/api/note";

  currentNote: Note = {};

  @Output()
  currentNoteChanged = new EventEmitter<Note>();

  constructor(private httpClient: HttpClient) {}

  public getNames(): Observable<string[]>  {
    return this.httpClient.get<string[]>(this.backendUrl);
  }

  public get(noteName: string): Observable<Note> {
    return this.httpClient.get<Note>(`${this.backendUrl}/${noteName}`)
  }

  public select(noteName: string): void {
    if (noteName === "") {
      this.currentNote = {};
      this.currentNoteChanged.emit(this.currentNote);
    }
    
    this.get(noteName).subscribe(note => {
      this.currentNote = note;
      this.currentNoteChanged.emit(note);
    });
  }

  public create(fileName: string): Observable<Note> {
    return this.httpClient.post<Note>(`${this.backendUrl}/${fileName}`, null);
  }

  public delete(fileName: string): Observable<any> {
    return this.httpClient.delete(`${this.backendUrl}/${fileName}`);
  }

  public save(note: Note): Observable<Note> {
    return this.httpClient.put<Note>(this.backendUrl, note);
  }
}
