import { CustomersDetailsComponent } from './CustomerDetails/CustomersDetailsComponent';
import { CustomersListComponent } from './CustomersList/customersListComponent';
import { OrganizationRoutingModule } from './oraganization-routing.module';
import { SharedModule } from './../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

@NgModule({
   imports: [
    CommonModule,
    SharedModule,
    OrganizationRoutingModule
  ],
  declarations: [CustomersListComponent, CustomersDetailsComponent]
})
export class OrganizationModule { }