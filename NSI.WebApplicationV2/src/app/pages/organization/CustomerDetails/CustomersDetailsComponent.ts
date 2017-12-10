import { Component, OnInit, OnDestroy } from '@angular/core';
import { Customer } from '../models/customer';
import { CustomersService } from '../../../services/customers.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector:'customer-details',
    templateUrl:'./customer-details.html'
})
export class CustomersDetailsComponent implements OnInit {

	id: number;
	customer: Customer;
	private sub: any;

	constructor(private customersService: CustomersService, private route: ActivatedRoute) {
	}

	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
			this.id = +params['id'];
			this.customersService.getCustomer(this.id).subscribe(data => {
				this.customer = data;
				console.log(this.customer);
			});
		});
	}

	ngOnDestroy() {
		this.sub.unsubscribe();
	}
}
