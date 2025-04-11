import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../Services/User/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-forgotpassword',
  standalone: false,
  templateUrl: './forgotpassword.component.html',
  styleUrl: './forgotpassword.component.scss'
})
export class ForgotpasswordComponent implements OnInit{
forgotpassForm!:FormGroup

 constructor(
    private router: Router,
    private formbuild: FormBuilder, 
    private user: UserService,
    private snackBar: MatSnackBar
  ) { }
  ngOnInit(): void {
     this.forgotpassForm = this.formbuild.group({
          email: ['', [Validators.required, Validators.email]]       
        });
  }


  forgotPass(){
    if (this.forgotpassForm.invalid) {
      this.forgotpassForm.markAllAsTouched();
      this.snackBar.open('Please fill all required fields correctly', 'Close', { duration: 3000 });
      return;
    }
    let reqData = {
      email: this.forgotpassForm.value.email
    }

    this.user.forgotPassword(reqData).subscribe({
      next:(res:any)=>{
        console.log('Reset password Link sent to your mail');
        this.snackBar.open('Reset-password link sent to your mail', 'Close', { duration: 3000 });
      }, 
      error: (err) => {
        console.error(err);
        this.snackBar.open(' Failed. Please try again.', 'Close', { duration: 3000 });
      }}
    )

  }
}
