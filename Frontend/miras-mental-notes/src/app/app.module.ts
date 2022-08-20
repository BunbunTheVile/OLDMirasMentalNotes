import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NoteService } from './services/note.service';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NoteListComponent } from './note-list/note-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NoteListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [ NoteService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
