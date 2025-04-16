import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UpdateNotesComponent } from '../update-notes/update-notes.component';
import { NotesService } from '../../Services/Note/notes.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ViewServiceService } from '../../Services/ViewService/view-service.service';
import { Observable } from 'rxjs';

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
  selector: 'app-trash-notes',
  standalone: false,
  templateUrl: './trash-notes.component.html',
  styleUrl: './trash-notes.component.scss'
})
export class TrashNotesComponent implements OnInit {
   isGridView$: Observable<boolean>;
  @Output() retry = new EventEmitter<void>();
  notes: Note[] = [];
  loading = true;
  error = '';

  // notesObject:any
  selectedNote: Note | null = null;

  constructor(public dialog: MatDialog, private notesService: NotesService,private snackBar: MatSnackBar,public viewService: ViewServiceService) { 
    this.isGridView$ = this.viewService.viewState$;
  }

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
          })).filter((note: Note) => note.isDeleted);
        
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


  // Method to handle note deletion
  deleteNoteForever(note: Note) {
    

    if (!note) {
      console.error('No note selected for deletion!');
      return;
    }
    const id= note.notes_id;
    this.notesService.deleteNotes(id).subscribe({
      next: (res: any) => {
        console.log('Note trashed successfully', res);
        this.snackBar.open('Note Deleted Forever Successfully','',{duration:5000});
        this.fetchNotes();

      },
      error: (err) => {
        console.error('Error trashing note:', err);
        this.snackBar.open('Note Deletion Failed','',{duration:5000});
      }
    });
  }
  restoreNote(note:Note){
    if (!note) {
      console.error('No note selected for deletion!');
      return;
    }
    const noteId= note.notes_id;
    this.notesService.trashNotes(noteId).subscribe({
      next: (res: any) => {
        console.log('Note Restored successfully', res);
        this.snackBar.open('Note restored Successfully','',{duration:5000});
        this.fetchNotes();
        

      },
      error: (err) => {
        console.error('Error restoring note:', err);
        this.snackBar.open('Note restoration Failed','',{duration:5000});
      }
    });
  }
  onRetry() {
    this.retry.emit();
  }
}
