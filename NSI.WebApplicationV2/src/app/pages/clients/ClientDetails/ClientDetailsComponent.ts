import { Component, OnInit, OnDestroy } from '@angular/core';
import { Client } from '../models/client';
import { ClientsService } from '../../../services/clients.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Customer } from '../../organization/models/customer';
import { CustomersService } from '../../../services/customers.service';
import { Address } from '../../address/address.model';
import { AddressService} from '../../../services/address.service';

@Component({
    selector:'client-details',
    templateUrl:'./client-details.html'
})

export class ClientDetailsComponent implements OnInit {

	id: number;

	client: Client;
	private sub: any;
	customers:Customer[];
	addresses: Address [];

	constructor(private clientsService: ClientsService, private route: ActivatedRoute, private router:Router, private customersService: CustomersService, private addressService: AddressService) {
	}

	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
				this.customersService.getCustomers().subscribe(customeri=>{
					this.addressService.getAddreses().subscribe(adrese=>{
						this.customers=customeri;
						this.addresses=adrese;
						this.id = +params['id'];												
						this.clientsService.getClient(this.id).subscribe(klijent => {
							this.client = klijent;							
					});
				});	
			});
		});
	}

	ngOnDestroy() {
		this.sub.unsubscribe();
	}

	cancel() {
		this.router.navigate(['/clients']);
	}

	update() {
		this.router.navigate(['/clients']);
	}
}