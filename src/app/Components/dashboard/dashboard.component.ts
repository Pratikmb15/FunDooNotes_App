import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../Services/dataService/data.service';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  title = "Keep ";
  searchText = " ";

  constructor(private router: Router,private data:DataService){}

  navItems = [
    { name: "Notes", icon: "lightbulb_outline",route: '/Home' },
    { name: "Reminders", icon: "notifications_none",route: '/Home/Notifications' },
    { name: "Edit labels", icon: "edit",route: '/Home/Edit' },
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

}
