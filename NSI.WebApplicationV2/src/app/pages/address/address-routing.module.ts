import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddressComponent } from './address.component';
import { extract } from '../../core/services/i18n.service';
import {AddressNewComponent} from './address-new/address-new.component';
import {AddressListComponent} from './address-list/address-list.component';
import {AddressTypeNewComponent} from './address-type-new/address-type-new.component';
import {AddressTypeListComponent} from './address-type-list/address-type-list.component';

const routes: Routes = [
  { path: '', component: AddressComponent, data: { title: extract('Address') } },
  { path: 'new', component: AddressNewComponent },
  { path: 'list', component: AddressListComponent },
  { path: 'type/new', component: AddressTypeNewComponent },
  { path: 'type/list', component: AddressTypeListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddressRoutingModule { }
