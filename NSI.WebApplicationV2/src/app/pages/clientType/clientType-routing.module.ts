import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { ClientTypeComponent } from './clientType.component';
import { extract } from '../../core/services/i18n.service';
import { ClientTypeComponent } from './clientType.component';

const routes: Routes = [
  { path: '', component: ClientTypeComponent, data: { title: extract('ClientType') } },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientTypeRoutingModule { }
