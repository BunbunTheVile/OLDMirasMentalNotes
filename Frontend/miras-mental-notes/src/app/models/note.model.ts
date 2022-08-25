export interface Note {
    file?: string;
    content?: string;
}

export interface NoteListItem {
    file: string;
    deletionStarted: boolean;
}