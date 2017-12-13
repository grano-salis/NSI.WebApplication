import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TasksRoutingModule } from './tasks-routing.module';
import { TasksComponent } from "./tasks.component";

@NgModule({
  imports: [
    CommonModule,
    TasksRoutingModule
  ],
  declarations: [TasksComponent]
})
export class TasksModule { }
