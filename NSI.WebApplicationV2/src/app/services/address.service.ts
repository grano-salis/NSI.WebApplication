import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {Address} from '../pages/address/address.model';
import { AddressSearchCriteria } from '../pages/address/addressSearchCriteria.model';

@Injectable()
export class AddressService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/address';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getAddreses(params?: any): Observable<any> {
    return this.http.get(`${this._url}`);
  }

  getAddress(id: number): Observable<Address> {
    return this.http.get(`${this._url}/${id}`).map(data => {return <Address>(data)});
  }

  postAddress(address: Address): Observable<any> {
    const body = JSON.stringify(address);
    return this.http.post( environment.serverUrl + '/api/address', body, {headers: this.headers});
  }

  updateAddress(address: Address): Observable<any> {
    const body = JSON.stringify(address);
    return this.http.put( environment.serverUrl + '/api/address/' + address.addressId, body, {headers: this.headers});
  }

  deleteAddress(address: Address): Observable<any> {
    const body = JSON.stringify(address);
    return this.http.delete( environment.serverUrl + '/api/address/' + address.addressId, {headers: this.headers});
  }

  getSortedAddresses(criteria: AddressSearchCriteria, addresses: Address[]): Address[] {
    return addresses.sort((a,b) => {
      if(criteria.sortDirection === 'desc'){
        if (a[criteria.sortColumn] < b[criteria.sortColumn]){
          return 1;
        }
        else return -1;
      }
      else {
        if(a[criteria.sortColumn] > b[criteria.sortColumn]){
          return 1;
        }
        else return -1;
      }
    });
  }

}
