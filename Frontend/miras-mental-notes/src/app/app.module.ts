import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NoteService } from './services/note.service';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NoteListComponent } from './note-list/note-list.component';
import { NoteDisplayComponent } from './note-display/note-display.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    NoteListComponent,
    NoteDisplayComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: "note/:file", component: NoteDisplayComponent }
    ])
  ],
  providers: [ NoteService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
