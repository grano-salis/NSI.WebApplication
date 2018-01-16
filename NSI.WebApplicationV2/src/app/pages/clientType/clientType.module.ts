import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import {FormsModule} from '@angular/forms';
import { ClientTypeComponent } from './clientType.component';
import { ClientTypeRoutingModule } from './clientType-routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ClientTypeRoutingModule,
    FormsModule
  ],
  declarations: [ClientTypeComponent]
})
export class ClientTypeModule { }
