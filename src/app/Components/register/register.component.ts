import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';
@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  registerForm !:FormGroup
    constructor(private formbuild: FormBuilder, private user: UserService) { }
    ngOnInit(): void {
      this.registerForm = this.formbuild.group({
        FirstName:[''],
        LastName: [''],
        email: [''],
        password: ['']
      })
    }
    Register() {
      let reqData = {
        FirstName:this.registerForm.value.FirstName,
        LastName: this.registerForm.value.LastName,
        email: this.registerForm.value.email,
        password: this.registerForm.value.password
      }
      return this.user.Register(reqData).subscribe((res:any)=>console.log(res))
    }

}
