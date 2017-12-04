import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpHeaders, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Meeting} from '../pages/meetings/meeting-new/meeting';

@Injectable()
export class MeetingsService {

  private readonly _url: string;
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this._url = environment.serverUrl + '/api/meetings';
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
  }

  postMeeting(meeting: Meeting): Observable<any> {
    let body = JSON.stringify(meeting);
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.post(this._url, body, {headers: headers});

  }

  putMeeting(id: number, meeting: Meeting): Observable<any>{
    let body = JSON.stringify(meeting);
    let headers = new HttpHeaders({'Content-Type': 'application/json'});

    return this.http.put(this._url + "/" + id, body, {headers: headers});
  }

  getMeetings(params?: any): Observable<any> {
    return this.http.get(this._url);
  }

  getMeetingById(id: number): Observable<any>{

    return this.http.get(this._url + "/" + id);
  }

  deleteMeetingById(id: number): Observable<any> {
    return this.http.delete(this._url + "/" + id);
  }

}
