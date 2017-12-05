import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Hearing} from '../pages/hearings/hearing-new/hearing';

@Injectable()
export class HearingsService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/hearings';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  postHearing(hearing: Hearing): Observable<any> {
    let body = JSON.stringify(hearing);
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.post(this._url, body, {headers: headers});

  }

  putHearing(id: number, hearing: Hearing): Observable<any>{
    let body = JSON.stringify(hearing);
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.put(this._url + "/" + id, body, {headers: headers});
  }

  getHearingById(id: number): Observable<any>{
    return this.http.get(this._url + "/" + id);
  }

}
