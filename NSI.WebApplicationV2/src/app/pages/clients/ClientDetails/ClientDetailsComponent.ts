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
	createClient: boolean;

	constructor(private clientsService: ClientsService, private route: ActivatedRoute, private router:Router, private customersService: CustomersService, private addressService: AddressService) {
	}

	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
				this.customersService.getCustomers().subscribe(customeri=>{
					this.addressService.getAddreses().subscribe(adrese=>{
						this.customers=customeri;
						this.addresses=adrese;

						if (params['id'] == 'new') 
						{
							this.createClient = true;
							var adrId = this.addresses[0].addressId;
							var orgId = this.customers[0].customerId;
					
							//this.client=new Client("",new Date(), 1, adrId, orgId, 1);
							this.client={clientName:"", dateCreated: new Date(), clientTypeId:1, addressId: adrId, customerId: orgId, createdByUserId:1};
						} 
						
						else 
						{
							this.createClient = false;
							this.id = +params['id'];
							this.clientsService.getClient(this.id).subscribe(klijent => {
								this.client = klijent;
							});		
						}
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
		this.clientsService.updateClient(this.client).subscribe(data=>{
			this.router.navigate(['/clients']);			
		});
	}

	create(){
		//this.client.clientTypeId=Number(this.client.clientTypeId);
		//this.client.createdByUserId=1;
				
		this.clientsService.createClient(this.client).subscribe(data=>{
			this.router.navigate(['/clients']);
		});
	}
}