import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Meeting } from '../pages/meetings/meeting-new/meeting';

@Injectable()
export class MeetingsService {

    private readonly _url: string;

    constructor(private http: HttpClient) {
        this._url = environment.serverUrl + '/api/meetings/';
    }

    postMeeting(meeting: Meeting): Observable<any> {
        let body = JSON.stringify(meeting);
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });

        return this.http.post(this._url, body, { headers: headers });

    }

}
