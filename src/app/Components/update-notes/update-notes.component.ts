import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IncomingMessage } from 'node:http';
import { NotesService } from '../../Services/Note/notes.service';

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
  selector: 'app-update-notes',
  standalone: false,
  templateUrl: './update-notes.component.html',
  styleUrl: './update-notes.component.scss'
})
export class UpdateNotesComponent {

  Description: string;
  Title: string;
  id: number; // Ensure this is correctly extracted
  Color: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Note, // Use the Note interface
    public dialogbox: MatDialogRef<UpdateNotesComponent>,
    private notesService: NotesService
  ) {
    this.Title = data.title;
    this.Description = data.description;
    this.id = data.notes_id; // Ensure this matches the property name in the Note interface
    this.Color = data.color;
  }

  
  closeDialog() {
    let reqData = {
      title: this.Title,
      description: this.Description,
      color: this.Color // Ensure this matches the backend model
    };

    this.notesService.updateNotes(reqData, this.id).subscribe(
      (res: any) => {
        console.log(res);
        this.dialogbox.close();
      },
      (error) => {
        console.error('Error updating note:', error);
      }
    );
  }
}
