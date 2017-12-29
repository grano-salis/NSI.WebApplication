import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { TasksRoutingModule } from './tasks-routing.module';
import { TasksComponent } from "./tasks.component";
import { NewTaskComponent } from './new-task/new-task.component';
import { FormsModule } from "@angular/forms";
import { TasksOverviewComponent } from './tasks-overview/tasks-overview.component';
declare var $: any;
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    TasksRoutingModule,
    FormsModule
  ],
  declarations: [TasksComponent, NewTaskComponent, TasksOverviewComponent]
})
export class TasksModule { }
