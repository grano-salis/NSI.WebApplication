import { CustomersDetailsComponent } from './CustomerDetails/CustomersDetailsComponent';
import { CustomersListComponent } from './CustomersList/customersListComponent';
import { CustomersService } from '../../services/customers.service';
import { PricingPackagesService } from '../../services/pricing-package.service';
import { OrganizationRoutingModule } from './oraganization-routing.module';
import { SharedModule } from './../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

@NgModule({
   imports: [
    CommonModule,
    SharedModule,
		FormsModule,
    OrganizationRoutingModule
  ],
  declarations: [CustomersListComponent, CustomersDetailsComponent],
	providers: [CustomersService, PricingPackagesService]
})
export class OrganizationModule { }
