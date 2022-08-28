export interface Note {
    name?: string;
    content?: string;
    tags?: string[];
}

export interface NoteListItem {
    name: string;
    deletionStarted: boolean;
}