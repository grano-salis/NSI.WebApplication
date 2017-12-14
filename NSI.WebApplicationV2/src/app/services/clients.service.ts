import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Client } from '../pages/clients/models/client';

@Injectable()
export class ClientsService {

  private readonly _url: string;
  private headers = new HttpHeaders();
  

  constructor(private http: HttpClient) {
	this._url = environment.serverUrl;
	this.headers = new HttpHeaders({'Content-Type': 'application/json'});	
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

	updateClient(id:number, newClient:Client): Observable<any> {
		let body=JSON.stringify(newClient);

		return this.http.put(`${this._url}/api/client/${id}`,body, {headers:this.headers});
	}
}
