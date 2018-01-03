import { Component, OnInit } from '@angular/core';
import {TasksService} from "../../../services/tasks.service";
import {AlertService} from "../../../services/alert.service";
import * as moment from "moment";
@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

  params: any = {
    page: 1,
    pageSize: 5,
    dateFrom: moment('2016-01-01').toJSON(),
    dateTo: moment(new Date()).toJSON()
  };

  tasks: any;

  taskStatuses: any = ['Active', 'Closed', 'In progress'];
  constructor(private tasksService: TasksService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.loadData()
  }

  private loadData() {
    this.tasksService.getTasksWithDueDateRange(this.params)
      .subscribe((response: any) => {
        this.tasks = response;
      }, e => this.alertService.showError(e.error.message));
  }

  changeTaskStatus(task: any){
    console.log("task:", task);
    this.tasksService.putTask(task.taskId, task)
      .subscribe((r:any) => {
        this.loadData();
      });
  }
  pageChanged(event: any){
    this.params.page = event.page;
    this.loadData();
  }
}
