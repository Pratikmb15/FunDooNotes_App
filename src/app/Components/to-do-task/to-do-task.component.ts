import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-to-do-task',
  standalone: false,
  templateUrl: './to-do-task.component.html',
  styleUrl: './to-do-task.component.scss'
})
export class ToDoTaskComponent implements OnInit{
  tasks:{task:string,done:boolean}[]=[];
  newtask:string='';

  constructor() {}

  ngOnInit() {
    const savedTasks = localStorage.getItem('tasks');
    if (savedTasks) {
      this.tasks = JSON.parse(savedTasks);
    }
  }

  addTask(){

    if(this.newtask){
      this.tasks.push({task:this.newtask,done:false});
      this.newtask='';
      this.updateLocalStorage();
    }
  }
  toggleTaskStatus(index:number){
    this.tasks[index].done=!this.tasks[index].done;
    this.updateLocalStorage;
  }
  deleteTask(index:number){
    this.tasks.splice(index, 1);
    this.updateLocalStorage();
  }
  updateLocalStorage() {
    localStorage.setItem('tasks', JSON.stringify(this.tasks));
  }
}
