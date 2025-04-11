import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  constructor(private router: Router, private formbuild: FormBuilder, private user: UserService, private snackBar: MatSnackBar) { }
  ngOnInit(): void {
    this.loginForm = this.formbuild.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    })
  }
  Login() {
    if (this.loginForm.invalid) {
      // Mark all fields as touched to show errors
      this.loginForm.markAllAsTouched();
      this.snackBar.open('Please fill all required fields correctly', 'Close', { duration: 3000 });
      return;
    }
    let reqData = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    }
    return this.user.Login(reqData).subscribe({
      next: (res: any) => {
        console.log(res)
        localStorage.setItem("token", res.token);
        this.snackBar.open('Login Successful', '', { duration: 5000 });
        this.router.navigate(['/Home']);
      }
      , error: (err) => {
        console.error('Login Failed :', err);
        this.snackBar.open('Login Failed !', '', { duration: 5000 });
        if (err.error) {
          console.error('Server Response:', err.error);
        }
      }
    })

  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  navigateToRegister() {
    this.router.navigate(['/Register']);
  }
  navigateToForgotPass(){
    this.router.navigate(['/Forgotpassword']);
  }

}
