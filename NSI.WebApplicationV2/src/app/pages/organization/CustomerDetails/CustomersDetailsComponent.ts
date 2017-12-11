import { Component, OnInit, OnDestroy } from '@angular/core';
import { Customer } from '../models/customer';
import { CustomersService } from '../../../services/customers.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector:'customer-details',
    templateUrl:'./customer-details.html'
})
export class CustomersDetailsComponent implements OnInit {

	id: number;
	customer: Customer;
	newOrg: boolean;
	private sub: any;

	constructor(private customersService: CustomersService, private route: ActivatedRoute, private router: Router) {
	}

	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
			if (params['id'] == 'new') {
				this.newOrg = true;
				this.customer = { customerName: "", customerId: -1, dateCreated: new Date(), pricingPackage: null, address: null , logoLink: ""};
			} else {
				this.newOrg = false;
				this.id = +params['id'];
				this.customersService.getCustomer(this.id).subscribe(data => {
					this.customer = data;
				});
			}
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
