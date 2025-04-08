import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;

  constructor(
    private router: Router,
    private formbuild: FormBuilder, 
    private user: UserService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.registerForm = this.formbuild.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$')
      ]]
    });
  }

  // Helper methods for template
  get firstName() {
    return this.registerForm.get('FirstName');
  }

  get lastName() {
    return this.registerForm.get('LastName');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  Register() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      this.snackBar.open('Please fill all required fields correctly', 'Close', { duration: 3000 });
      return;
    }
    
    let reqData = {
      FirstName: this.registerForm.value.FirstName,
      LastName: this.registerForm.value.LastName,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password
    }
    
    this.user.Register(reqData).subscribe({
      next: (res: any) => {
        console.log(res);
        this.router.navigate(['/Login']);
      },
      error: (err) => {
        console.error(err);
        this.snackBar.open('Registration failed. Please try again.', 'Close', { duration: 3000 });
      }
    });
  }
}