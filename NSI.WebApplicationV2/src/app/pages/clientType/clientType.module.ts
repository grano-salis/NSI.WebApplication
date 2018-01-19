import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { ClientTypeComponent } from './clientType.component';
import { ClientTypeRoutingModule } from './clientType-routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ClientTypeRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [ClientTypeComponent]
})
export class ClientTypeModule { }
