import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Customer } from '../pages/organization/models/customer';

@Injectable()
export class CustomersService {

  private readonly _url: string;
  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
   }

   getCustomers(params?: any): Observable<Customer[]> {
	   return this.http.get(`${this._url}/api/customer`).map(res => {
			 return <Customer[]>(res);
	   });
   }

	deleteCustomer(id: number): Observable<any> {
		return this.http.delete(`${this._url}/api/customer/${id}`);
	}

	getCustomer(id: number): Observable<Customer> {
		return this.http.get(`${this._url}/api/customer/${id}`).map(res => {
			return <Customer>(res);
		});
	}

	updateCustomer(customer: Customer): Observable<any> {
		return this.http.put(`${this._url}/api/customer/${customer.customerId}`, customer);
	}

	createCustomer(customer: Customer): Observable<any> {
		return this.http.post(`${this._url}/api/customer`, customer);
	}


}
