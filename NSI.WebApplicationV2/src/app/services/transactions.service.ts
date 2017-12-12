import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import {HttpClient, HttpHeaders, HttpRequest} from '@angular/common/http';


@Injectable()
export class TransactionsService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/transactions';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getTransactions():Observable<any[]>{
    return this.http.get<any[]>(this._url);
  }

  postTransaction(transaction: any): Observable<any> {
    let body = JSON.stringify(transaction);
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.post(this._url, body, {headers: headers});

  }
}
