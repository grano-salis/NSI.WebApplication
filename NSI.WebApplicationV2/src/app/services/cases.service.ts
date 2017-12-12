import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {Case} from '../pages/cases/new-case/case';

@Injectable()
export class CasesService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/case/info';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getCases(params?: any): Observable<any> {
    return this.http.get(this._url);
  }

  getCaseById(caseId: any){
    return this.http.get(this._url + '/' + caseId);
  }

  postCase(caseToSend: Case): Observable<any> {
    const body = JSON.stringify(caseToSend);
    console.log('body', body);
    return this.http.post( this._url, body, {headers: this.headers});
  }

  deleteCase(id: number): Observable<any> {
    return this.http.delete(this._url + '/' + id);
  }

  putCase(id: number, caseToPut: Case): Observable<any> {
    const body = JSON.stringify(caseToPut);
    const headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.put(this._url + '/' + id, body, {headers: headers});
  }
}
