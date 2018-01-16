import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Tasks } from './tasks';
import { TasksService } from '../../../services/tasks.service';
import { UsersService } from '../../../services/users.service';
import { ActivatedRoute } from '@angular/router';
declare var $: any;

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

  onSubmit() {
    console.log(this.model);
    this.model.dueDate = $('#dueDate').val();
    this.model.title = $('#title').val();
    this.model.description = $('#description').val();
    console.log(this.model);
    this.tasksService.postTasks(this.model).subscribe((r: any) => console.log(r),
      (error: any) => console.log("Error: " + error.message));
  }

  newTask() {
    this.model = new Tasks();
  }

  ngOnInit() { }
}
