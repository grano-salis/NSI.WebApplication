import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MeetingsComponent } from './meetings.component';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: MeetingsComponent, data: { title: extract('Meetings') } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MeetingsRoutingModule { }
