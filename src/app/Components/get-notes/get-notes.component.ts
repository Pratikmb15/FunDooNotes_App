import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../Services/Note/notes.service';
import { DisplayNotesComponent } from "../display-notes/display-notes.component";

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
  selector: 'app-get-notes',
  standalone: false,
  templateUrl: './get-notes.component.html',
  styleUrl: './get-notes.component.scss',
 
})
export class GetNotesComponent implements OnInit {
  notes: Note[] = [];
  loading = true;
  error = '';

  constructor(private notesService: NotesService) {}

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
            isPinned: false, // Default value
            isSelected: false, // Default value
          }));
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
}