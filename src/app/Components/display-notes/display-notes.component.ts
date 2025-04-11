import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UpdateNotesComponent } from '../update-notes/update-notes.component';
import { DataService } from '../../Services/dataService/data.service';
import { response } from 'express';

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
  selector: 'app-display-notes',
  standalone: false,
  templateUrl: './display-notes.component.html',
  styleUrl: './display-notes.component.scss',
})
export class DisplayNotesComponent implements OnInit {
  @Input() notes: Note[] = [];
  @Input() loading: boolean = false;
  @Input() error: string = '';
  @Output() retry = new EventEmitter<void>();
  @Output() UpdateAutoRefresh = new EventEmitter();
  @Output() refreshRequested = new EventEmitter<void>();

  // notesObject:any
  selectedNote: Note | null = null;
  filterNote: any;

  constructor(public dialog: MatDialog, private data: DataService) { }

  ngOnInit(): void {
    this.data.incomingData.subscribe((response) => {
      console.log("Search in progress", response);
      this.filterNote = response;
    })

  }
  editNoteDialogBox(note: Note) {
    const dialogbox = this.dialog.open(UpdateNotesComponent, {
      width: '40%',
      height: 'auto',
      data: note // Pass the specific note object
    });

    dialogbox.afterClosed().subscribe(result => {
      console.log(result);
      this.UpdateAutoRefresh.emit(result);
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
  recievedRefreshEventActions($event:any){
    console.log("Icon buttons to getall notes"+$event);
    this.UpdateAutoRefresh.emit();
  }
  onRefreshRequested() {
    this.refreshRequested.emit();
  }
}