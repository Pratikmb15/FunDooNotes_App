import { Component, type OnInit } from "@angular/core"
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
      transition("collapsed <=> expanded", [animate("200ms ease-in-out")]),
    ]),
  ]
})
export class NotesComponent  implements OnInit{
  noteForm !:FormGroup
  isExpanded = false
  noteTitle = ""
  noteContent = ""
  
  constructor(private formbuild: FormBuilder,private note: NotesService) {}

  ngOnInit(): void {
    this.noteForm = this.formbuild.group({
      Title: [''],
      Description: ['']
    })

  }
  

  expandNote() {
    this.isExpanded = true
  }
  closeNote(event: Event)  {
    event.preventDefault();
    let reqData = {
      title: this.noteForm.get('Title')?.value,
      description: this.noteForm.get('Description')?.value 
    };
  
    console.log("Request Data:", reqData); 
  
    if (!reqData.title || !reqData.description) {
      console.error("Error: Title or Description is missing!");
      return;
    }
  
    this.note.addNotes(reqData).subscribe(
      (res: any) => {
        console.log("Response:", res);
        this.isExpanded = false; 
        this.noteForm.reset(); 
      },
      (error) => {
        console.error("Error Response:", error);
      }
    );
    
    this.isExpanded = false;
    this.noteForm.reset();
  }
  
}
