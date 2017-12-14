import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { extract } from "../../core/services/i18n.service";
import { TasksComponent } from "./tasks.component";
import { NewTaskComponent } from './new-task/new-task.component';
import { TasksOverviewComponent } from './tasks-overview/tasks-overview.component';

const routes: Routes = [
  { path: '', component: TasksComponent, data: { title: extract('Tasks') }},
  { path: 'new', component: NewTaskComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasksRoutingModule { }
