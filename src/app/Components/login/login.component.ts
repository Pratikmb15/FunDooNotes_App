import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';


@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  loginForm !: FormGroup
  constructor(private formbuild: FormBuilder, private user: UserService) { }
  ngOnInit(): void {
    this.loginForm = this.formbuild.group({
      email: [''],
      password: ['']
    })
  }
  Login() {

    let reqData = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    }
    return this.user.Login(reqData).subscribe((res: any) => {
      console.log(res)
      localStorage.setItem("token", res.token)
    })
  }
}
