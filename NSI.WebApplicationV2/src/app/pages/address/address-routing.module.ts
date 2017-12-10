import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddressComponent } from './address.component';
import { extract } from '../../core/services/i18n.service';
import {HearingNewComponent} from '../hearings/hearing-new/hearing-new.component';
import {AddressNewComponent} from './address-new/address-new.component';
import {AddressListComponent} from './address-list/address-list.component';

const routes: Routes = [
  { path: '', component: AddressComponent, data: { title: extract('Address') } },
  { path: 'new', component: AddressNewComponent },
  { path: 'list', component: AddressListComponent },
 // { path: 'new', component: AddressTypeNewComponent },
 // { path: 'list', component: AddressTypeListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddressRoutingModule { }
