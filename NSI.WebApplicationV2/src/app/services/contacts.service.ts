import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {Contact} from "../pages/contacts/new-contact/contact";

@Injectable()
export class ContactsService {

  private readonly _url: string;
  private headers = new HttpHeaders();
  constructor(private http: HttpClient) {
    this._url = environment.serverUrl;
    this.headers = new HttpHeaders({'Content-Type': 'application/json'});
   }

   getContacts(params?: any): Observable<any> {
     return this.http.get(`${this._url}/api/contacts`);
   }

  postContact(contact: Contact): Observable<any> {
    const body = JSON.stringify(contact);
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    console.log(body);
    return this.http.post(`${this._url}/api/contacts`, body, {headers: headers});

  }


}
