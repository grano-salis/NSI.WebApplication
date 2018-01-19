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
	addressIds: number[];
  pricingPackageIds: number[];
  searchName: string="";
  tempArray: Customer[];

	constructor(private customersService: CustomersService, private addressService: AddressService, private pricingPackageService: PricingPackagesService, private router: Router) {
	}

	ngOnInit() {
		this.addresses = new Array<Address>();
		this.pricingPackages = new Array<PricingPackage>();
		this.addressIds = new Array<number>();
		this.pricingPackageIds = new Array<number>();
		this.customersService.getCustomers().subscribe(data => {
			this.customers = data;
			this.tempArray = data;
			for(let customer of this.customers) {
				if (customer.addressId && this.addressIds.indexOf(customer.addressId) === -1) {
					this.addressIds.push(customer.addressId);
					this.addressService.getAddress(customer.addressId).subscribe(data => {
						this.addresses.push(data);
					});
				}
				if (customer.pricingPackageId && this.pricingPackageIds.indexOf(customer.pricingPackageId) === -1) {
					this.pricingPackageIds.push(customer.pricingPackageId);
					this.pricingPackageService.getPricingPackage(customer.pricingPackageId).subscribe(data => {
						this.pricingPackages.push(data);
					});
				}
			}
		});
	}

	getAddress(addressId: number) {
		for(let address of this.addresses) {
			if(address.addressId === addressId) {
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

  search(){
	  this.customers = this.tempArray.filter((customer: Customer) => {
	    return customer.customerName.indexOf(this.searchName) !== -1;
    });
	  console.log(this.searchName);
  }

	onCustomerClick(id: number) {
		this.router.navigate([`/organization/${id}`]);
	}
}
