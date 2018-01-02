import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { TasksRoutingModule } from './tasks-routing.module';
import { TasksComponent } from "./tasks.component";
import { NewTaskComponent } from './new-task/new-task.component';
import { FormsModule } from "@angular/forms";
import { TasksOverviewComponent } from './tasks-overview/tasks-overview.component';
import { CurrentComponent } from './current/current.component';
import { HistoryComponent } from './history/history.component';
import {PaginationModule} from "ngx-bootstrap";
declare var $: any;
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    TasksRoutingModule,
    FormsModule,
    PaginationModule
  ],
  declarations: [TasksComponent, NewTaskComponent, TasksOverviewComponent, CurrentComponent, HistoryComponent]
})
export class TasksModule { }
