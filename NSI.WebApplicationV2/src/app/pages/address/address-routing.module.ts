import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddressComponent } from './address.component';
import { extract } from '../../core/services/i18n.service';

const routes: Routes = [
  { path: '', component: AddressComponent, data: { title: extract('Address') } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddressRoutingModule { }
