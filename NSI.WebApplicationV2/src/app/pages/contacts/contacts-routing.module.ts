import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContactsComponent } from './contacts.component';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: ContactsComponent, data: { title: extract('Contacts') } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContactsRoutingModule { }
