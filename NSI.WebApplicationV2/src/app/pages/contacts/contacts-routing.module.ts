import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ContactsComponent} from './contacts.component';
import {extract} from '../../core/services/i18n.service';
import {NewContactComponent} from './new-contact/new-contact.component';

const routes: Routes = [
  {path: '', component: ContactsComponent, data: {title: extract('Contacts')}},
  {path: 'new', component: NewContactComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContactsRoutingModule {
}
