import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../Services/User/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-resetpassword',
  standalone: false,
  templateUrl: './resetpassword.component.html',
  styleUrl: './resetpassword.component.scss'
})
export class ResetpasswordComponent implements OnInit{
token:any
 ressetpassForm!:FormGroup
 constructor(
  private router: Router,
  private formbuild: FormBuilder, 
  private user: UserService,
  private snackBar: MatSnackBar,
  private activateRoute:ActivatedRoute
) { }
  ngOnInit(): void {
    this.ressetpassForm = this.formbuild.group({
    
      newPassWord: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$')
      ]],
      confirmPassWord:['',Validators.required]
    });
  }


  ressetpass(){
    if (this.ressetpassForm.invalid) {
      this.ressetpassForm.markAllAsTouched();
      this.snackBar.open('Please fill all required fields correctly', 'Close', { duration: 3000 });
      return;
    }

    let reqData = {
      newPassWord: this.ressetpassForm.value.newPassWord,
      confirmPassWord:this.ressetpassForm.value.confirmPassWord
    }
    this.token=this.activateRoute.snapshot.queryParamMap.get('token')
    console.log(reqData,this.token);
    this.user.ressetPassword(reqData,this.token).subscribe({
      next:(res:any)=>{
        console.log(' password Reset Successfully');
        this.snackBar.open('password Reset Successfully', 'Close', { duration: 3000 });
        this.router.navigate(['/Login']);
      }, 
      error: (err) => {
        console.error(err);
        this.snackBar.open(' Failed resseting Password. Please try again.', 'Close', { duration: 3000 });
      }})
  }

}
