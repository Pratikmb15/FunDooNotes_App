import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  title = "Keep ";
  searchText = " ";

  navItems = [
    { name: "Notes", icon: "lightbulb_outline" },
    { name: "Reminders", icon: "notifications_none" },
    { name: "Edit labels", icon: "edit" },
    { name: "Archive", icon: "archive" },
    { name: "Trash", icon: "delete" },
  ]

}
