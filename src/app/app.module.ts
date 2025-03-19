import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations"

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { RegisterComponent } from './Components/register/register.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { ForgotpasswordComponent } from './Components/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './Components/resetpassword/resetpassword.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {MatToolbarModule }  from '@angular/material/toolbar';
import {MatSidenavModule }  from '@angular/material/sidenav';
import {MatIconModule }  from '@angular/material/icon';
import {MatListModule }  from '@angular/material/list';
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { ToDoTaskComponent } from './Components/to-do-task/to-do-task.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { NotesComponent } from './Components/notes/notes.component';
import { MatMenuModule } from "@angular/material/menu";

import { MatMenuTrigger } from "@angular/material/menu";
import { MatTooltipModule } from "@angular/material/tooltip";
import { TextFieldModule } from "@angular/cdk/text-field";
import { DisplayNotesComponent } from './Components/display-notes/display-notes.component';
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { IconButtonsComponent } from './Components/icon-buttons/icon-buttons.component';
import { GetNotesComponent } from './Components/get-notes/get-notes.component';
import { UpdateNotesComponent } from './Components/update-notes/update-notes.component';
import {MatDialogModule} from '@angular/material/dialog';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ForgotpasswordComponent,
    ResetpasswordComponent,
    DashboardComponent,
    ToDoTaskComponent,
    NotesComponent,
    DisplayNotesComponent,
    IconButtonsComponent,
    GetNotesComponent,
    UpdateNotesComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatCardModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatMenuModule,
    MatTooltipModule,
    TextFieldModule,
    MatMenuTrigger,
    MatProgressSpinnerModule,
    MatDialogModule,
    
  ],
  providers: [
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
