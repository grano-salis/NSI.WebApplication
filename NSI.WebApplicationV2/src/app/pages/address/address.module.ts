import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddressRoutingModule } from './address-routing.module';
import { AddressComponent } from './address.component';
import { SharedModule } from '../../shared/shared.module';
import { AddressNewComponent } from './address-new/address-new.component';
import { AddressListComponent } from './address-list/address-list.component';
import {FormsModule} from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AddressRoutingModule,
    FormsModule
  ],
  declarations: [AddressComponent, AddressNewComponent, AddressListComponent]
})
export class AddressModule { }
