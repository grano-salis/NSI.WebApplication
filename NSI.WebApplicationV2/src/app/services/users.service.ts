import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders, HttpRequest, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Meeting} from '../pages/meetings/meeting-new/meeting';

@Injectable()
export class UsersService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/users';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  getForMeetings(username:string) : Observable<any> {
      let params = new HttpParams();
      params = params.append('username', username);
      return this.http.get(this._url + '/meetings', { headers: this.headers, params: params});
  }

  getForHearings(username:string) : Observable<any> {
    let params = new HttpParams();
    params = params.append('username', username);
    return this.http.get(this._url + '/hearings', { headers: this.headers, params: params});
}

}
