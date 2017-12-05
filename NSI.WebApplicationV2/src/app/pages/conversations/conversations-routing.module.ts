import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConversationsComponent } from './conversations.component';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: ConversationsComponent, data: { title: extract('Conversations') } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConversationsRoutingModule { }