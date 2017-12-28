import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {
  currentTasks: boolean;
  tasksHistory: boolean;
  constructor() { }

  ngOnInit() {
    this.currentTasks = true;
  }

}
