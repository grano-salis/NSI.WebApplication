import { ClientDetailsComponent } from './ClientDetails/ClientDetailsComponent';
import { ClientsListComponent } from './ClientsList/ClientsListComponent';
import { ClientsService } from '../../services/clients.service';
import { ClientsRoutingModule } from './clients-routing.module';
import { SharedModule } from './../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Customer } from '../organization/models/customer';
import { CustomersService } from '../../services/customers.service';
import { AddressService } from '../../services/address.service';

@NgModule({
   imports: [
    CommonModule,
    SharedModule,
	FormsModule,
    ClientsRoutingModule
  ],
  declarations: [ClientsListComponent, ClientDetailsComponent],
  providers: [ClientsService, CustomersService, AddressService]
})
export class ClientsModule { }
