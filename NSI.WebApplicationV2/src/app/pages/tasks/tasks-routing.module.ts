import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TasksComponent} from "./tasks.component";
import {NewTaskComponent} from './new-task/new-task.component';
import {CurrentComponent} from "./current/current.component";
import {HistoryComponent} from "./history/history.component";

const routes: Routes = [
  {
    path: '', component: TasksComponent, children: [
      {path: '', redirectTo: 'current', pathMatch: 'full'},
      {path: 'new', component: NewTaskComponent},
      {path: 'current', component: CurrentComponent},
      {path: 'history', component: HistoryComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasksRoutingModule {
}
