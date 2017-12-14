import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Tasks } from './tasks';
import { TasksService } from '../../../services/tasks.service';
import { UsersService } from '../../../services/users.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.scss']
})
export class NewTaskComponent implements OnInit {
  ngAfterViewInit(): void {
    $('#dueDate').datetimepicker();
  }
  model: Tasks;
  constructor(private tasksService: TasksService, private usersService: UsersService, private route: ActivatedRoute) {
    this.model = new Tasks();
  }
  ngOnInit() { }
}
