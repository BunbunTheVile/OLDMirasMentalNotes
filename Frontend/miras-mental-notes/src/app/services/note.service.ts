import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Note } from 'src/app/models/note.model';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  backendUrl: string = "https://localhost:7001/api/note";

  constructor(private httpClient: HttpClient) {}

  public GetNames(): Observable<string[]>  {
    return this.httpClient.get<string[]>(this.backendUrl);
  }

  public Get(fileName: string): Observable<Note> {
    return this.httpClient.get<Note>(`${this.backendUrl}/${fileName}`)
  }
}
