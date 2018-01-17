import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Client } from '../pages/clients/models/client';

@Injectable()
export class ClientsService {

  private readonly _url: string;
  private headers = new HttpHeaders({'Content-Type': 'application/json'});
  

  constructor(private http: HttpClient) {
	this._url = environment.serverUrl;
   }

   getClients(params?: any): Observable<any> {
	   return this.http.get(`${this._url}/api/client`);
   }

	deleteClient(id: number): Observable<any> {
		return this.http.delete(`${this._url}/api/client/${id}`);
	}

	getClient(id: number): Observable<any> {
		return this.http.get(`${this._url}/api/client/${id}`);
	}

	updateClient(newClient:Client): Observable<any> {
		var body='?ClientId='+newClient.clientId+'&ClientName='+newClient.clientName+'&ClientTypeId='+newClient.clientTypeId+'&CustomerId='+newClient.customerId+'&AddressId='+newClient.addressId+'&CreatedByUserId='+newClient.createdByUserId;
		return this.http.put(`${this._url}/api/client`+body, {headers: this.headers});
	}

	createClient(newClient:Client):Observable<any> {
		//return this.http.post(`${this._url}/api/client`,newClient, {headers: this.headers});

		var body='?ClientName='+newClient.clientName+'&ClientTypeId='+newClient.clientTypeId+'&CustomerId='+newClient.customerId+'&AddressId='+newClient.addressId+'&CreatedByUserId='+newClient.createdByUserId;
		console.log(body);
		return this.http.post(`${this._url}/api/client`+body, {headers:this.headers});
	}
}
