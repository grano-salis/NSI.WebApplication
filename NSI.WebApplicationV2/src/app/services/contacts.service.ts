import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ContactsService {

  private readonly _url: string;
  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
   }

   getContacts(params?: any): Observable<any> {
     return this.http.get(`${this._url}/api/contacts`);
   }

}
