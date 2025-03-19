import { Component, Input, OnInit } from '@angular/core';
import { NotesService } from '../../Services/Note/notes.service';


@Component({
  selector: 'app-icon-buttons',
  standalone: false,
  templateUrl: './icon-buttons.component.html',
  styleUrl: './icon-buttons.component.scss',

})
export class IconButtonsComponent implements OnInit{
  @Input() notesObject: any;

  Color:any;
  selectedColor: string = ''; 

  colorArray: Array<any> = [
    { code: '#ffffff', name: 'white' },
    { code: '#FF6347', name: 'Tomato' },
    { code: '#FF4500', name: 'OrangeRed'},
    { code: '#FFFF00', name: 'yellow' },
    { code: '#ADFF2F', name: 'greenyellow' },
    { code: '#B0C4DE', name: 'LightSteelBlue' },
    { code: '#EEE8AA', name: 'PaleGoldenRod' },
    { code: '#7FFFD4', name: 'Aquamarine' },
    { code: '#FFE4C4', name: 'Bisque' },
    { code: '#C0C0C0', name: 'Silver' },
    { code: '#BC8F8F', name: 'RosyBrown' },
    { code: '#D3D3D3', name: 'grey' },
  ];

  constructor(private notesService:NotesService){}
  ngOnInit(): void {
  }
  onDelete() {
    if (!this.notesObject || !this.notesObject.notes_id) {
      console.error("Note object is undefined or missing 'notes_id'");
      return;
    }
  
    const noteId = this.notesObject.notes_id; 
    console.log(noteId);
  
    this.notesService.trashNotes(noteId).subscribe({
      next: (res: any) => {
        console.log('Note trashed successfully', res);
      },
      error: (err) => {
        console.error('Error trashing note:', err);
      }
    });
  }
  
  onArchive(){
    if (!this.notesObject || !this.notesObject.notes_id) {
      console.error("Note object is undefined or missing 'notes_id'");
      return;
    }
    const noteId = this.notesObject.notes_id; // Extract the notes_id
    console.log(noteId);
   
    this.notesService.ArchiveNotes(noteId).subscribe((res:any)=>{
      console.log('Note Archieved successfully ',res);
    }) 
  }

  SelectColor(colors:any){
    let reqData = {
      color: colors.code // Ensure this matches the backend model
    };
    this.selectedColor=colors.code

    if (!this.notesObject || !this.notesObject.notes_id) {
      console.error("Note object is undefined or missing 'notes_id'");
      return;
    }

    const noteId = this.notesObject.notes_id; 
    console.log(noteId);
    this.notesService.UpdateNotesColor(noteId,reqData).subscribe(
      (res: any) => {
        console.log(res);
       
      },
      (error) => {
        console.error('Error updating note color:', error);
      }
    );

  }

}
