import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NotesService } from '../../Services/Note/notes.service';
import { Observable } from 'rxjs';
import { ViewServiceService } from '../../Services/ViewService/view-service.service';

interface Note {
  notes_id: number;
  title: string;
  description: string;
  color: string;
  isDeleted: boolean;
  isArchive: boolean;
  id: number;
  isPinned?: boolean; 
  isSelected?: boolean; 
}

@Component({
  selector: 'app-archive-notes',
  standalone: false,
  templateUrl: './archive-notes.component.html',
  styleUrl: './archive-notes.component.scss'
})
export class ArchiveNotesComponent implements OnInit {
  isGridView$: Observable<boolean>;
  @Output() retry = new EventEmitter<void>();
  // @Output() refreshRequest = new EventEmitter<void>();
  notes: Note[] = [];
  loading = true;
  error = '';

  // notesObject:any
  selectedNote: Note | null = null;

  constructor(public dialog: MatDialog, private notesService: NotesService,public viewService: ViewServiceService) {
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

  handleRefresh() {
    
    this.fetchNotes(); // Your existing data fetching method
  }
  recievedRefreshFromDisplaytoGetall($event:any){
    console.log("display to getall notes"+ $event);
    this.fetchNotes();
  }
 
}
