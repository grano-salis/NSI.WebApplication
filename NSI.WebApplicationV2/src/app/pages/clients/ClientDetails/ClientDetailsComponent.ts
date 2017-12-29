import { Component, OnInit, OnDestroy } from '@angular/core';
import { Client } from '../models/client';
import { ClientsService } from '../../../services/clients.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';


@Component({
    selector:'client-details',
    templateUrl:'./client-details.html'
})

export class ClientDetailsComponent implements OnInit {

	id: number;
	client: Client = undefined;
	private sub: any = [];

	constructor(private clientsService: ClientsService, private route: ActivatedRoute, private router:Router) {
	}

	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
			this.id = +params['id'];
			this.clientsService.getClient(this.id).subscribe(data => {
				this.client = data;
				console.log(this.client);
			});
		});
	}

	ngOnDestroy() {
		this.sub.unsubscribe();
	}

	cancel() {
		this.router.navigate(['/clients']);
	}
}