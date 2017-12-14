import { Component, OnInit } from '@angular/core';
import { Client } from '../models/client';
import { ClientsService } from '../../../services/clients.service';
import { Router } from '@angular/router';

@Component({
    selector:'clients-list',
    templateUrl:'./clients-list.html'
})

export class ClientsListComponent implements OnInit {

	clients: Client[];

	constructor(private clientsService: ClientsService, private router: Router) {
	}

	ngOnInit() {
		this.clientsService.getClients().subscribe(data => this.clients = data);
	}

	onDeleteClick(id: number, event: any) {
		event.stopPropagation();
		this.clientsService.deleteClient(id).subscribe(data => {
			console.log("briseeem clienta "+data);
			
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
}
