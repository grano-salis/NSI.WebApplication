import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AddressType } from '../pages/address/addressType.model';

@Injectable()
export class AddressTypeService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  selectedAddressType: AddressType; 

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/addresstype';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getAddressTypes(params?: any): Observable<any> {
    return this.http.get(`${this._url}`);
  }
  getAddressTypesById(AddressTypeId: any){
    return this.http.get(this._url + '/' + AddressTypeId);
  }

  postAddressType(addressType: AddressType): Observable<any> {
    const body = JSON.stringify(addressType);
    return this.http.post( environment.serverUrl + '/api/addresstype', body, {headers: this.headers});
  }

  deleteAddressType(params?: number): Observable<any> {
    return this.http.delete( environment.serverUrl + '/api/addresstype/' + params.toString());
  }
  putAddressType(id: number, typeAddress: AddressType): Observable<any> {
    console.log('put service');
    console.log(id);
    console.log(typeAddress);
    const body = JSON.stringify(typeAddress);
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.put(this._url + '/' + id, body, {headers: headers});
  }


}
