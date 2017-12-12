import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ShellComponent} from '../core/shell/shell.component';

const routes: Routes = [
  // {path: 'login', loadChildren: './login/login.module#LoginModule'}, posto mi koristimo SSO onda nam ne treba ovaj dio
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {
    path: '',
    component: ShellComponent,
    children: [
      {path: 'home', loadChildren: './home/home.module#HomeModule'},
      {path: 'about', loadChildren: './about/about.module#AboutModule'},
      {path: 'meetings', loadChildren: './meetings/meetings.module#MeetingsModule'},
      {path: 'organization', loadChildren:'./organization/oraganization.module#OrganizationModule'},
      {path: 'contacts', loadChildren: './contacts/contacts.module#ContactsModule'},
      {path: 'documents', loadChildren: './documents/documents.module#DocumentsModule'},
      {path: 'address', loadChildren: './address/address.module#AddressModule'},
      {path: 'hearings', loadChildren: './hearings/hearings.module#HearingsModule'},
      {path: 'conversations/:id', loadChildren: './conversations/conversations.module#ConversationsModule'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class PagesRoutingModule {
}
