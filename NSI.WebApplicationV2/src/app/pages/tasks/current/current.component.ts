import { Component, OnInit } from '@angular/core';
import {TasksService} from "../../../services/tasks.service";
import {AlertService} from "../../../services/alert.service";
import { each } from 'lodash';
import * as moment from "moment";
@Component({
  selector: 'app-current',
  templateUrl: './current.component.html',
  styleUrls: ['./current.component.scss']
})
export class CurrentComponent implements OnInit {

  tasks: any;

  constructor(private tasksService: TasksService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.loadData()
  }

  private loadData() {
    this.tasksService.getTasks()
      .subscribe((response: any) => {
        this.tasks = response;
      }, e => this.alertService.showError(e.error.message));
  }
}
