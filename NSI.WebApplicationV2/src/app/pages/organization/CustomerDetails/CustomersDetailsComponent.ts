import { Component, OnInit, OnDestroy } from '@angular/core';
import { Customer } from '../models/customer';
import { PricingPackage } from '../models/pricing-package';
import { CustomersService } from '../../../services/customers.service';
import { PricingPackagesService } from '../../../services/pricing-package.service';
import { AddressService } from '../../../services/address.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Address } from '../../address/address.model';
import { Location } from '@angular/common';

@Component({
    selector:'customer-details',
    templateUrl:'./customer-details.html'
})
export class CustomersDetailsComponent implements OnInit {

	id: number;
	customer: Customer;
	pricingPackages: PricingPackage[];
	addresses: Address[];
	newOrg: boolean;
	private sub: any;

	constructor(private customersService: CustomersService, private pricingPackagesService: PricingPackagesService, private addressService: AddressService, private route: ActivatedRoute, private router: Router, private currentLocation: Location) {
	}

	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
			this.pricingPackagesService.getActivePricingPackages().subscribe(data => {
				this.addressService.getAddreses().subscribe(addressRes => {
					this.addresses = addressRes;
					this.pricingPackages = data;
					if (params['id'] == 'new') {
						this.newOrg = true;
						var prcId = this.pricingPackages[0].pricingPackageId;
						var adrId = this.addresses[0].addressId;
						this.customer = { customerName: "", addressId: adrId, logoLink: "", dateCreated: new Date(), pricingPackageId: prcId};
					} else {
						this.newOrg = false;
						this.id = +params['id'];
						this.customersService.getCustomer(this.id).subscribe(data => {
							this.customer = data;
						});
					}
				});

			});
		});
	}

	onUpdateClick() {
		this.customersService.updateCustomer(this.customer).subscribe(data => {
			console.log(data);
			this.router.navigate([`/organization`]);
		});
	}

	onCancelClick() {
		this.currentLocation.back();
	}

	onSubmitClick() {
		this.customersService.createCustomer(this.customer).subscribe(data => {
			console.log(data);
			this.router.navigate([`/organization`]);
		});
	}


	ngOnDestroy() {
		this.sub.unsubscribe();
	}
}
