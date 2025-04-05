import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  loginForm !: FormGroup
  constructor(private router: Router,private formbuild: FormBuilder, private user: UserService,private snackBar: MatSnackBar ) { }
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
      localStorage.setItem("token", res.token);
      this.snackBar.open('Login Successful','',{duration:5000});
      this.router.navigate(['/Home']);
    })
  }
}
