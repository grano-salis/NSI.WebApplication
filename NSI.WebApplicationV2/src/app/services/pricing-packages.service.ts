import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import {HttpClient, HttpHeaders, HttpRequest} from '@angular/common/http';

@Injectable()
export class PricingPackagesService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/pricingpackage';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getPricingPackages(): Observable<any[]>{
    return this.http.get<any[]>(this._url);
  }

  getPricingPackageById(id: number): Observable<any>{
    return this.http.get<any>(this._url+"/"+id);
  }


}
