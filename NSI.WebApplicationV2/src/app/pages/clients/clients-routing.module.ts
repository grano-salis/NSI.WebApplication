import { ClientDetailsComponent } from './ClientDetails/ClientDetailsComponent';
import { ClientsListComponent } from './ClientsList/ClientsListComponent';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: ClientsListComponent, data: { title: extract('Clients') } },
  { path: ':id', component: ClientDetailsComponent, data: { title: extract('Name')}},
  { path: 'new', component: ClientDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientsRoutingModule { }
