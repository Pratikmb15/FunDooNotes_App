import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../Services/dataService/data.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ViewServiceService } from '../../Services/ViewService/view-service.service';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  isGridView = true;
  title = "Keep ";
  searchText = " ";

  constructor(private router: Router,private data:DataService, private snackBar: MatSnackBar,public viewService: ViewServiceService){}

  navItems = [
    { name: "Notes", icon: "lightbulb_outline",route: '/Home' },
    { name: "Archive", icon: "archive",route: '/Home/Archive' },
    { name: "Trash", icon: "delete", route: '/Home/Trash' },
  ]

  navigateTo(route: string) {
    this.router.navigate([route]);
  }

  search(event:any){
    console.log(event.target.value);
    this.data.outgoingData(event.target.value);
  }

  logOut(){
    try{
    localStorage.removeItem("token");
    console.log('user logged out successfully ');
    this.snackBar.open('Logged out successfully', 'Close', { duration: 3000 });
    this.router.navigate(['/Login']);

  }catch(Error:any){
    console.log('Log out failed ');
    
  }
  }
  
toggleView() {
  this.viewService.toggleView();
}

}
