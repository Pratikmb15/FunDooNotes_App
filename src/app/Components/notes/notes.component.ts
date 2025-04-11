import { Component, Output, type OnInit, EventEmitter } from "@angular/core";
import { trigger, state, style, transition, animate } from "@angular/animations";
import { FormBuilder, FormGroup } from '@angular/forms';
import { NotesService } from '../../Services/Note/notes.service';

@Component({
  selector: 'app-notes',
  standalone: false,
  templateUrl: './notes.component.html',
  styleUrl: './notes.component.scss',
  animations: [
    trigger("expandCollapse", [
      state(
        "collapsed",
        style({
          height: "46px",
          boxShadow: "none",
        }),
      ),
      state(
        "expanded",
        style({
          minHeight: "180px",
          boxShadow: "0 3px 5px rgba(0,0,0,0.2)",
        }),
      ),
      transition("collapsed <=> expanded", [animate("50ms ease-in-out")]),
    ]),
  ]
})
export class NotesComponent implements OnInit {
  @Output() refreshEventCreate = new EventEmitter();
  
  noteForm!: FormGroup;
  isExpanded = false;
  noteTitle = "";
  noteContent = "";

  Color:any;
  selectedColor: string = '#ffffff'; 

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

  
  constructor(private formbuild: FormBuilder, private note: NotesService) {}

  ngOnInit(): void {
    this.noteForm = this.formbuild.group({
      Title: [''],
      Description: ['']
    });
  }

  expandNote() {
    this.isExpanded = true;
  }

  closeNote(event: Event) {
    event.preventDefault();
    
    // Only submit if there's content
    if (this.noteForm.value.Title || this.noteForm.value.Description) {
      let reqData = {
        title: this.noteForm.get('Title')?.value,
        description: this.noteForm.get('Description')?.value,
        color: this.selectedColor
      };

      console.log("Request Data:", reqData);

      this.note.addNotes(reqData).subscribe(
        (res: any) => {
          console.log("Response:", res);
          this.selectedColor="#ffffff";
          this.refreshEventCreate.emit(res);
        },
        (error) => {
          console.error("Error Response:", error);
        }
      );
    }
    
    // Always collapse and reset the form
    this.isExpanded = false;
    this.noteForm.reset();
  }
  SelectColor(color:any){
    this.selectedColor=color.code
  }
}