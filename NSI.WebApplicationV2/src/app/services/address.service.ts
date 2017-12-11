import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {Address} from '../pages/address/address.model';

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

  postAddress(address: Address): Observable<any> {
    const body = JSON.stringify(address);
    return this.http.post( environment.serverUrl + '/api/address', body, {headers: this.headers});
  }

}
