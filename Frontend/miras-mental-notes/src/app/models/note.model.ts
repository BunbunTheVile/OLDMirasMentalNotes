export interface Note {
    name?: string;
    content?: string;
}

export interface NoteListItem {
    name: string;
    deletionStarted: boolean;
}