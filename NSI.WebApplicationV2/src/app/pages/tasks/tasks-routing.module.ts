import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { extract } from "../../core/services/i18n.service";
import { TasksComponent } from "./tasks.component";

const routes: Routes = [
   { path: '', component: TasksComponent, data: { title: extract('Tasks') } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasksRoutingModule { }
