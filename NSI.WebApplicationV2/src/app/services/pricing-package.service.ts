import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { PricingPackage } from '../pages/organization/models/pricing-package';

@Injectable()
export class PricingPackagesService {

  private readonly _url: string;
  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
   }

   getPricingPackages(params?: any): Observable<PricingPackage[]> {
	   return this.http.get(`${this._url}/api/PricingPackage`).map(res => {
			 return <PricingPackage[]>(res);
	   });
   }

   getActivePricingPackages(params?: any): Observable<PricingPackage[]> {
	   return this.http.get(`${this._url}/api/PricingPackage/Active`).map(res => {
			 return <PricingPackage[]>(res);
	   });
   }

}
