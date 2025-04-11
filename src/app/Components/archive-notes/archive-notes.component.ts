import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UpdateNotesComponent } from '../update-notes/update-notes.component';
import { NotesService } from '../../Services/Note/notes.service';

interface Note {
  notes_id: number;
  title: string;
  description: string;
  color: string;
  isDeleted: boolean;
  isArchive: boolean;
  id: number;
  isPinned?: boolean; // UI state
  isSelected?: boolean; // UI state
}

@Component({
  selector: 'app-archive-notes',
  standalone: false,
  templateUrl: './archive-notes.component.html',
  styleUrl: './archive-notes.component.scss'
})
export class ArchiveNotesComponent implements OnInit {
  @Output() retry = new EventEmitter<void>();
  // @Output() refreshRequest = new EventEmitter<void>();
  notes: Note[] = [];
  loading = true;
  error = '';

  // notesObject:any
  selectedNote: Note | null = null;

  constructor(public dialog: MatDialog, private notesService: NotesService) { }

  ngOnInit(): void {
    this.fetchNotes();
  }

  fetchNotes(): void {
    this.loading = true;
    this.notesService.getNotes().subscribe({
      next: (response: any) => {
        console.log('Notes response:', response);
        if (response && response.success && response.data && response.data.notes) {
          // Add UI state properties to each note
          this.notes = response.data.notes.map((note: Note) => ({
            ...note,
            isPinned: false,
            isSelected: false
          })).filter((note: Note) => note.isArchive && !note.isDeleted);
        
        } else {
          this.error = 'Invalid response format';
          console.error('Invalid response format:', response);
        }
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching notes:', err);
        this.error = 'Failed to load notes. Please try again later.';
        this.loading = false;
      },
    });
  }
  editNoteDialogBox(note: Note) {
    const dialogbox = this.dialog.open(UpdateNotesComponent, {
      width: '40%',
      height: 'auto',
      data: note // Pass the specific note object
    });

    dialogbox.afterClosed().subscribe(result => {
      console.log(result);

    });
  }

  togglePin(note: Note) {
    note.isPinned = !note.isPinned;
  }

  setSelectedNote(note: Note, event: Event) {
    event.stopPropagation();
    this.selectedNote = note;
  }

  toggleSelect(note: Note, event: Event) {
    event.stopPropagation();
    note.isSelected = !note.isSelected;
  }

  // Method to handle note deletion
  deleteNote(note: Note | null, event: Event) {
    event.stopPropagation();

    if (!note) {
      console.error('No note selected for deletion!');
      return;
    }

    note.isDeleted = true;
    console.log('Note trashed:', note);
  }
  onRetry() {
    this.retry.emit();
  }
  onRefreshRequested() {
    this.fetchNotes();
  }
}
