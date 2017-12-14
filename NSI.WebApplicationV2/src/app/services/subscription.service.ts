import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';

@Injectable()
export class SubscriptionService{

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/subscription';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getSubscriptions():Observable<any[]>{
    return this.http.get<any[]>(this._url);
  }

  getActiveSubscription(customerId:number):Observable<any[]>{
    return this.http.get<any>(this._url+"/active/"+customerId);
  }

  postSubscription(subscription: any): Observable<any> {
    let body = JSON.stringify(subscription);
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.post(this._url, body, {headers: headers});

  }

  updateSubscription(subscription: any): Observable<any> {
    let body = JSON.stringify(subscription);
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.put(this._url, body, {headers: headers});
  }

  

}
