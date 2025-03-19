import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { ForgotpasswordComponent } from './Components/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './Components/resetpassword/resetpassword.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { ToDoTaskComponent } from './Components/to-do-task/to-do-task.component';
import { NotesComponent } from './Components/notes/notes.component';
import { DisplayNotesComponent } from './Components/display-notes/display-notes.component';
import { IconButtonsComponent } from './Components/icon-buttons/icon-buttons.component';
import { GetNotesComponent } from './Components/get-notes/get-notes.component';

const routes: Routes = [
  {path:'Login',component:LoginComponent},
  {path:'Register',component:RegisterComponent},
  {path:'Forgotpassword',component:ForgotpasswordComponent},
  {path:'Resetpassword',component:ResetpasswordComponent},
  {path:'ToDo',component:ToDoTaskComponent},
  {path:'get-notes',component:GetNotesComponent},
  {
    path: 'Home',
    component: DashboardComponent,
    children: [
      { path: '', component: GetNotesComponent }, // Child route of NotesComponent
      //{ path: '', redirectTo: 'notes', pathMatch: 'full' }, 
    ],
  },
  { path: 'display-notes', component: DisplayNotesComponent },
  { path: 'icon-buttons', component: IconButtonsComponent },
  { path: '', redirectTo: '/Login', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: '/Login' }, // Fallback route for unknown paths


];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
