import { Component, OnInit, OnDestroy } from '@angular/core';
import { Customer } from '../models/customer';
import { PricingPackage } from '../models/pricing-package';
import { CustomersService } from '../../../services/customers.service';
import { PricingPackagesService } from '../../../services/pricing-package.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Address } from '../../address/address.model';

@Component({
    selector:'customer-details',
    templateUrl:'./customer-details.html'
})
export class CustomersDetailsComponent implements OnInit {

	id: number;
	customer: Customer;
	pricingPackages: PricingPackage[];
	newOrg: boolean;
	private sub: any;

	constructor(private customersService: CustomersService, private pricingPackagesService: PricingPackagesService, private route: ActivatedRoute, private router: Router) {
	}

	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
			this.pricingPackagesService.getActivePricingPackages().subscribe(data => {
				this.pricingPackages = data;
				if (params['id'] == 'new') {
					this.newOrg = true;
					this.customer = { customerName: "", customerId: -1, dateCreated: new Date(), pricingPackage: this.pricingPackages[0], address: new Address() , logoLink: ""};
				} else {
					this.newOrg = false;
					this.id = +params['id'];
					this.customersService.getCustomer(this.id).subscribe(data => {
						this.customer = data;
					});
				}
			});
		});
	}

	onUpdateClick() {
		this.customersService.updateCustomer(this.customer).subscribe(data => {
			console.log(data);
			this.router.navigate([`/organization`]);
		});
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
