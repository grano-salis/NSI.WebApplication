import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { extract } from '../../core/services/i18n.service';
import { HearingNewComponent } from './hearing-new/hearing-new.component';

const routes: Routes = [
  { path: 'new', component: HearingNewComponent },
  { path: 'edit/:id', component: HearingNewComponent},
  { path: 'delete/:id', component: HearingNewComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: []
})
export class HearingsRoutingModule { }
