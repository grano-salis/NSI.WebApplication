import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddressRoutingModule } from './address-routing.module';
import { SharedModule } from '../../shared/shared.module';
import {FormsModule} from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AddressRoutingModule,
    FormsModule
  ],
  declarations: []
})
export class AddressTypeModule { }
