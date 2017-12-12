import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customer';
import { Address } from '../../address/address.model';
import { PricingPackage } from '../models/pricing-package';
import { CustomersService } from '../../../services/customers.service';
import { AddressService } from '../../../services/address.service';
import { PricingPackagesService } from '../../../services/pricing-package.service';
import { Router } from '@angular/router';

@Component({
    selector:'customer-list',
    templateUrl:'./customer-list.html'
})
export class CustomersListComponent implements OnInit {

	customers: Customer[];
	addresses: Address[];
	pricingPackages: PricingPackage[];

	constructor(private customersService: CustomersService, private addressService: AddressService, private pricingPackageService: PricingPackagesService, private router: Router) {
	}

	ngOnInit() {
		this.customersService.getCustomers().subscribe(data => this.customers = data);
		this.addresses = new Array<Address>();
		this.pricingPackages = new Array<PricingPackage>();
		this.addressService.getAddreses().subscribe(data => this.addresses = data);
		this.pricingPackageService.getPricingPackages().subscribe(data => this.pricingPackages = data);
	}

	getAddress(addressId: number) {
		for(let address of this.addresses) {
			if(address.addres_id === addressId) {
				return address;
			}
		}
		return null;
	}

	getPricingPackage(pricingPackageId: number) {
		for(let pricingPackage of this.pricingPackages) {
			if(pricingPackage.pricingPackageId === pricingPackageId) {
				return pricingPackage;
			}
		}
		return null;
	}

	onDeleteClick(id: number, event: any) {
		event.stopPropagation();
		this.customersService.deleteCustomer(id).subscribe(data => {
			console.log(data);
			for(var i=0; i < this.customers.length; i++) {
				if (this.customers[i].customerId === id) {
					this.customers.splice(i, 1);
					break;
				}
			}
		});
	}

	onCustomerClick(id: number) {
		this.router.navigate([`/organization/${id}`]);
	}
}
