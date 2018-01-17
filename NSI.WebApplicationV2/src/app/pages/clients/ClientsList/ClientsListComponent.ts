import { Component, OnInit } from '@angular/core';
import { Client } from '../models/client';
import { ClientsService } from '../../../services/clients.service';
import { Router } from '@angular/router';
import { Address } from '../../address/address.model';
import { Customer } from '../../organization/models/customer';
import { CustomersService } from '../../../services/customers.service';
import { AddressService } from '../../../services/address.service';

@Component({
    selector:'clients-list',
    templateUrl:'./clients-list.html'
})

export class ClientsListComponent implements OnInit {

	clients: Client[];
	customers:Customer[];
	addresses: Address[];
	addressIds: number[];
	customerIds: number[];
  searchName: string="";
  tempData: Client[];

	constructor(private clientsService: ClientsService, private customersService: CustomersService, private addressService: AddressService, private router: Router) {
	}

	ngOnInit() {
		this.addresses=new Array<Address> ();
		this.customers=new Array<Customer> ();
		this.addressIds=new Array<number> ();
		this.customerIds=new Array<number> ();

		this.clientsService.getClients().subscribe(data => {
			this.clients = data;
      this.tempData = data;
			for(let client of this.clients) {
				if (client.addressId && this.addressIds.indexOf(client.addressId) === -1) {
					this.addressIds.push(client.addressId);
					this.addressService.getAddress(client.addressId).subscribe(data => {
						this.addresses.push(data);
					});
				}
				if (client.customerId && this.customerIds.indexOf(client.customerId) === -1) {
					this.customerIds.push(client.customerId);
					this.customersService.getCustomer(client.customerId).subscribe(data => {
						this.customers.push(data);
					});
				}
			}
		});
	}

	onDeleteClick(id: number, event: any) {
		event.stopPropagation();
		this.clientsService.deleteClient(id).subscribe(data => {
			for(var i=0; i < this.clients.length; i++) {
				if (this.clients[i].clientId === id) {
					this.clients.splice(i, 1);
					break;
				}
			}
		});
	}

	onClientClick(id: number) {
		this.router.navigate([`/clients/${id}`]);
	}

	getAddress(addressId: number) {
		for(let address of this.addresses) {
			if(address.addressId === addressId) {
				return address;
			}
		}
		return null;
	}

	getCustomer(customerId: number) {
		for(let customer of this.customers) {
			if(customer.customerId === customerId) {
				return customer;
			}
		}
		return null;
	}

  search() {
    this.clients = this.tempData.filter((client: Client) => {
      return client.clientName.indexOf(this.searchName) !== -1;
    });
    console.log(this.searchName);
  }
}
