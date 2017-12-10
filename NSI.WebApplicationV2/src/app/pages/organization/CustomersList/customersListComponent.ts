import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customer';
import { CustomersService } from '../../../services/customers.service';
import { Router } from '@angular/router';

@Component({
    selector:'customer-list',
    templateUrl:'./customer-list.html'
})
export class CustomersListComponent implements OnInit {

	customers: Customer[];

	constructor(private customersService: CustomersService, private router: Router) {
	}

	ngOnInit() {
		this.customersService.getCustomers().subscribe(data => this.customers = data);
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
